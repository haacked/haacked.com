#!/usr/bin/env bats
# Tests for script/lib/config-loader.sh

setup() {
    # Get paths
    TEST_DIR="$(cd "$(dirname "$BATS_TEST_FILENAME")" && pwd)"
    PROJECT_ROOT="$(cd "$TEST_DIR/../.." && pwd)"
    SCRIPT="$PROJECT_ROOT/script/lib/config-loader.sh"

    # Source the script
    source "$SCRIPT"

    # Create temporary directory for test configs
    TEST_TEMP_DIR="$(mktemp -d)"
    export BLOG_ROOT="$TEST_TEMP_DIR"
}

teardown() {
    # Clean up temp directory
    [ -d "$TEST_TEMP_DIR" ] && rm -rf "$TEST_TEMP_DIR"
}

# === load_blog_config tests ===

@test "load_blog_config: loads valid config file" {
    cat > "$TEST_TEMP_DIR/config.yml" << 'EOF'
OPENAI_API_KEY="sk-test-key"
TINYPNG_API_KEY="tinypng-key"
IMAGES_REPO_PATH="$HOME/images"
IMAGE_BASE_URL="https://test.com"
EDITOR="vim"
EOF

    load_blog_config "$TEST_TEMP_DIR/config.yml" true

    [ "$OPENAI_API_KEY" = "sk-test-key" ]
    [ "$TINYPNG_API_KEY" = "tinypng-key" ]
    [ "$IMAGE_BASE_URL" = "https://test.com" ]
    [ "$EDITOR" = "vim" ]
}

@test "load_blog_config: expands \$HOME in paths" {
    cat > "$TEST_TEMP_DIR/config.yml" << 'EOF'
IMAGES_REPO_PATH="$HOME/images"
EOF

    load_blog_config "$TEST_TEMP_DIR/config.yml" true

    [ "$IMAGES_REPO_PATH" = "$HOME/images" ]
}

@test "load_blog_config: expands tilde in paths" {
    cat > "$TEST_TEMP_DIR/config.yml" << 'EOF'
IMAGES_REPO_PATH="~/images"
EOF

    load_blog_config "$TEST_TEMP_DIR/config.yml" true

    [[ "$IMAGES_REPO_PATH" =~ ^/.*images$ ]]
}

@test "load_blog_config: ignores comments" {
    cat > "$TEST_TEMP_DIR/config.yml" << 'EOF'
# This is a comment
OPENAI_API_KEY="sk-test-key"
# Another comment
EOF

    load_blog_config "$TEST_TEMP_DIR/config.yml" true

    [ "$OPENAI_API_KEY" = "sk-test-key" ]
}

@test "load_blog_config: ignores empty lines" {
    cat > "$TEST_TEMP_DIR/config.yml" << 'EOF'

OPENAI_API_KEY="sk-test-key"


EOF

    load_blog_config "$TEST_TEMP_DIR/config.yml" true

    [ "$OPENAI_API_KEY" = "sk-test-key" ]
}

@test "load_blog_config: warns on unknown keys" {
    cat > "$TEST_TEMP_DIR/config.yml" << 'EOF'
UNKNOWN_KEY="value"
EOF

    run load_blog_config "$TEST_TEMP_DIR/config.yml" true

    [[ "$output" =~ "Unknown configuration key" ]]
}

@test "load_blog_config: fails when required file missing" {
    run load_blog_config "$TEST_TEMP_DIR/nonexistent.yml" true

    [ "$status" -eq 1 ]
    [[ "$output" =~ "Configuration file not found" ]]
}

@test "load_blog_config: succeeds when optional file missing" {
    run load_blog_config "$TEST_TEMP_DIR/nonexistent.yml" false

    [ "$status" -eq 0 ]
}

@test "load_blog_config: handles single quotes" {
    cat > "$TEST_TEMP_DIR/config.yml" << EOF
OPENAI_API_KEY='sk-test-key'
EOF

    load_blog_config "$TEST_TEMP_DIR/config.yml" true

    [ "$OPENAI_API_KEY" = "sk-test-key" ]
}

@test "load_blog_config: handles values without quotes" {
    cat > "$TEST_TEMP_DIR/config.yml" << 'EOF'
DALLE_MODEL=dall-e-3
EOF

    load_blog_config "$TEST_TEMP_DIR/config.yml" true

    [ "$DALLE_MODEL" = "dall-e-3" ]
}

# === validate_editor tests ===

@test "validate_editor: accepts valid editor names" {
    run validate_editor "vim"
    [ "$status" -eq 0 ]

    run validate_editor "code"
    [ "$status" -eq 0 ]

    run validate_editor "/usr/bin/vim"
    [ "$status" -eq 0 ]
}

@test "validate_editor: rejects editor with special characters" {
    run validate_editor "vim; rm -rf /"
    [ "$status" -eq 1 ]
    [[ "$output" =~ "unsafe characters" ]]
}

@test "validate_editor: allows empty editor" {
    run validate_editor ""
    [ "$status" -eq 0 ]
}

@test "validate_editor: warns if editor not in PATH" {
    run validate_editor "nonexistent-editor-xyz"
    [ "$status" -eq 1 ]
    [[ "$output" =~ "not found in PATH" ]]
}

# === validate_path_safety tests ===

@test "validate_path_safety: allows paths within base directory" {
    mkdir -p "$TEST_TEMP_DIR/blog/images"
    touch "$TEST_TEMP_DIR/blog/images/test.png"

    result=$(validate_path_safety "$TEST_TEMP_DIR/blog/images/test.png" "$TEST_TEMP_DIR/blog" "test")

    [[ "$result" =~ /blog/images/test.png$ ]]
}

@test "validate_path_safety: rejects path traversal attempts" {
    mkdir -p "$TEST_TEMP_DIR/blog"

    run validate_path_safety "$TEST_TEMP_DIR/blog/../../etc/passwd" "$TEST_TEMP_DIR/blog" "test"

    [ "$status" -eq 1 ]
    [[ "$output" =~ "Path traversal detected" ]]
}

@test "validate_path_safety: prevents blog-evil bypass" {
    mkdir -p "$TEST_TEMP_DIR/blog"
    mkdir -p "$TEST_TEMP_DIR/blog-evil"

    # This should fail - blog-evil is NOT under blog/
    run validate_path_safety "$TEST_TEMP_DIR/blog-evil/file.txt" "$TEST_TEMP_DIR/blog" "test"

    [ "$status" -eq 1 ]
    [[ "$output" =~ "Path traversal detected" ]]
}

@test "validate_path_safety: allows exact base directory match" {
    mkdir -p "$TEST_TEMP_DIR/blog"

    result=$(validate_path_safety "$TEST_TEMP_DIR/blog" "$TEST_TEMP_DIR/blog" "test")

    [[ "$result" =~ /blog$ ]]
}

@test "validate_path_safety: requires proper directory boundary" {
    mkdir -p "$TEST_TEMP_DIR/blog"
    mkdir -p "$TEST_TEMP_DIR/blogosphere"

    # blogosphere is NOT under blog/, should fail
    run validate_path_safety "$TEST_TEMP_DIR/blogosphere/file.txt" "$TEST_TEMP_DIR/blog" "test"

    [ "$status" -eq 1 ]
}

# === get_file_size tests ===

@test "get_file_size: returns size of existing file" {
    echo "test content" > "$TEST_TEMP_DIR/test.txt"

    size=$(get_file_size "$TEST_TEMP_DIR/test.txt")

    [ "$size" -gt 0 ]
}

@test "get_file_size: works on empty file" {
    touch "$TEST_TEMP_DIR/empty.txt"

    size=$(get_file_size "$TEST_TEMP_DIR/empty.txt")

    [ "$size" -eq 0 ]
}

# === check_required_dependencies tests ===

@test "check_required_dependencies: succeeds with installed commands" {
    run check_required_dependencies bash cat ls

    [ "$status" -eq 0 ]
}

@test "check_required_dependencies: fails with missing commands" {
    run check_required_dependencies nonexistent-command-xyz

    [ "$status" -eq 1 ]
    [[ "$output" =~ "Missing required dependencies" ]]
}

@test "check_required_dependencies: function has install instructions for known tools" {
    # This test verifies that the function contains install instructions
    # We can't easily test the output without mocking, so we verify the function
    # contains the expected case statements for known tools

    # Read the function and check it has install instructions
    func_body=$(declare -f check_required_dependencies)

    # Check that function contains install instructions for jq
    [[ "$func_body" =~ "brew install jq" ]]
    [[ "$func_body" =~ "apt-get install jq" ]]
}
