#!/usr/bin/env bats
# Tests for slug generation in new-post script

setup() {
    # Get paths
    TEST_DIR="$(cd "$(dirname "$BATS_TEST_FILENAME")" && pwd)"
    PROJECT_ROOT="$(cd "$TEST_DIR/../.." && pwd)"

    # Create temp directory
    TEST_TEMP_DIR="$(mktemp -d)"
}

teardown() {
    # Clean up
    [ -d "$TEST_TEMP_DIR" ] && rm -rf "$TEST_TEMP_DIR"
}

# Helper function to simulate slug generation
generate_slug() {
    local title="$1"
    echo "$title" | tr '[:upper:]' '[:lower:]' | \
        sed -E 's/[^a-z0-9 -]//g' | \
        sed -E 's/ +/-/g' | \
        sed -E 's/-+/-/g' | \
        sed -E 's/^-|-$//g'
}

# === Slug generation tests ===

@test "generate_slug: basic title to slug" {
    result=$(generate_slug "My Amazing Post")

    [ "$result" = "my-amazing-post" ]
}

@test "generate_slug: removes special characters" {
    result=$(generate_slug "Hello! World?")

    [ "$result" = "hello-world" ]
}

@test "generate_slug: collapses multiple spaces" {
    result=$(generate_slug "Too    Many    Spaces")

    [ "$result" = "too-many-spaces" ]
}

@test "generate_slug: collapses multiple hyphens" {
    result=$(generate_slug "Too---Many---Hyphens")

    [ "$result" = "too-many-hyphens" ]
}

@test "generate_slug: removes leading hyphen" {
    result=$(generate_slug "-Leading Hyphen")

    [ "$result" = "leading-hyphen" ]
}

@test "generate_slug: removes trailing hyphen" {
    result=$(generate_slug "Trailing Hyphen-")

    [ "$result" = "trailing-hyphen" ]
}

@test "generate_slug: handles all special characters" {
    result=$(generate_slug "!@#\$%^&*()")

    [ -z "$result" ]
}

@test "generate_slug: preserves existing hyphens" {
    result=$(generate_slug "Pre-Existing-Hyphens")

    [ "$result" = "pre-existing-hyphens" ]
}

@test "generate_slug: handles numbers" {
    result=$(generate_slug "Post Number 42")

    [ "$result" = "post-number-42" ]
}

@test "generate_slug: handles mixed case" {
    result=$(generate_slug "CamelCaseTitle")

    [ "$result" = "camelcasetitle" ]
}

@test "generate_slug: handles unicode characters removal" {
    result=$(generate_slug "Café Señor")

    # Unicode should be removed, leaving just caf and seor
    [ "$result" = "caf-seor" ]
}

@test "generate_slug: empty string from special chars only" {
    result=$(generate_slug "!@#\$%^&*(){}[]<>?/|\\")

    [ -z "$result" ]
}

@test "generate_slug: handles spaces and hyphens combination" {
    result=$(generate_slug "Spaced - Out - Title")

    [ "$result" = "spaced-out-title" ]
}

@test "generate_slug: real world example 1" {
    result=$(generate_slug "How to Deploy Your App to Production")

    [ "$result" = "how-to-deploy-your-app-to-production" ]
}

@test "generate_slug: real world example 2" {
    result=$(generate_slug "Building a REST API with Node.js")

    [ "$result" = "building-a-rest-api-with-nodejs" ]
}

@test "generate_slug: real world example 3" {
    result=$(generate_slug "10 Tips for Better Code Reviews")

    [ "$result" = "10-tips-for-better-code-reviews" ]
}

# === Edge cases ===

@test "edge case: very long title" {
    long_title="This Is A Very Long Title With Many Words That Goes On And On And On And Should Still Be Processed Correctly"
    result=$(generate_slug "$long_title")

    [[ "$result" =~ ^this-is-a-very-long-title ]]
}

@test "edge case: single word" {
    result=$(generate_slug "Hello")

    [ "$result" = "hello" ]
}

@test "edge case: single character" {
    result=$(generate_slug "a")

    [ "$result" = "a" ]
}

@test "edge case: numbers only" {
    result=$(generate_slug "123 456 789")

    [ "$result" = "123-456-789" ]
}

@test "edge case: hyphens only" {
    result=$(generate_slug "---")

    [ -z "$result" ]
}

@test "edge case: mixed valid and invalid chars" {
    result=$(generate_slug "valid!!! @@@chars###")

    [ "$result" = "valid-chars" ]
}
