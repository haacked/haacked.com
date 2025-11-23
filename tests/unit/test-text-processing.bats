#!/usr/bin/env bats
# Tests for text processing functions in generate-images

setup() {
    # Get paths
    TEST_DIR="$(cd "$(dirname "$BATS_TEST_FILENAME")" && pwd)"
    PROJECT_ROOT="$(cd "$TEST_DIR/../.." && pwd)"

    # Source just the functions we need (not the whole script)
    # Extract functions into a testable module
    eval "$(sed -n '/^find_image_placeholders()/,/^}/p' "$PROJECT_ROOT/script/generate-images")"
    eval "$(sed -n '/^extract_placeholder_id()/,/^}/p' "$PROJECT_ROOT/script/generate-images")"
    eval "$(sed -n '/^extract_placeholder_description()/,/^}/p' "$PROJECT_ROOT/script/generate-images")"
    # extract_date_slug is now in config-loader.sh
    eval "$(sed -n '/^extract_date_slug()/,/^}/p' "$PROJECT_ROOT/script/lib/config-loader.sh")"
}

# === find_image_placeholders tests ===

@test "find_image_placeholders: finds single placeholder" {
    content="Here is [image1: A sunset] some text"

    result=$(find_image_placeholders "$content")

    [ "$result" = "[image1: A sunset]" ]
}

@test "find_image_placeholders: finds multiple placeholders" {
    content="[image1: First] text [image2: Second]"

    result=$(find_image_placeholders "$content")

    [ "$(echo "$result" | wc -l)" -eq 2 ]
}

@test "find_image_placeholders: handles no placeholders" {
    content="No placeholders here"

    result=$(find_image_placeholders "$content")

    [ -z "$result" ]
}

@test "find_image_placeholders: handles colons in description" {
    content="[image1: URL: https://example.com]"

    result=$(find_image_placeholders "$content")

    [ "$result" = "[image1: URL: https://example.com]" ]
}

@test "find_image_placeholders: handles multiline content" {
    content="Line 1
[image1: First image]
Line 2
[image2: Second image]"

    result=$(find_image_placeholders "$content")

    [ "$(echo "$result" | wc -l)" -eq 2 ]
}

@test "find_image_placeholders: requires image prefix with number" {
    content="[notimage: Wrong format]"

    result=$(find_image_placeholders "$content")

    [ -z "$result" ]
}

@test "find_image_placeholders: handles sequential numbers" {
    content="[image1: First] [image2: Second] [image99: Last]"

    result=$(find_image_placeholders "$content")

    [ "$(echo "$result" | wc -l)" -eq 3 ]
}

# === extract_placeholder_id tests ===

@test "extract_placeholder_id: extracts simple ID" {
    result=$(extract_placeholder_id "[image1: Description]")

    [ "$result" = "image1" ]
}

@test "extract_placeholder_id: handles different numbers" {
    result=$(extract_placeholder_id "[image42: Description]")

    [ "$result" = "image42" ]
}

@test "extract_placeholder_id: works with large numbers" {
    result=$(extract_placeholder_id "[image999: Description]")

    [ "$result" = "image999" ]
}

# === extract_placeholder_description tests ===

@test "extract_placeholder_description: extracts simple description" {
    result=$(extract_placeholder_description "[image1: A sunset]")

    [ "$result" = "A sunset" ]
}

@test "extract_placeholder_description: handles colons in description" {
    result=$(extract_placeholder_description "[image1: URL: https://example.com]")

    [ "$result" = "URL: https://example.com" ]
}

@test "extract_placeholder_description: trims leading spaces" {
    result=$(extract_placeholder_description "[image1:  Extra spaces]")

    [ "$result" = "Extra spaces" ]
}

@test "extract_placeholder_description: handles long descriptions" {
    input="[image1: This is a very long description with many words and details]"
    result=$(extract_placeholder_description "$input")

    [ "$result" = "This is a very long description with many words and details" ]
}

@test "extract_placeholder_description: handles special characters" {
    result=$(extract_placeholder_description "[image1: Test with \"quotes\" and 'apostrophes']")

    [[ "$result" =~ quotes ]]
}

# === extract_date_slug tests ===

@test "extract_date_slug: extracts from standard filename" {
    result=$(extract_date_slug "_posts/2025/2025-11-21-my-post.md")

    [ "$result" = "2025-11-21-my-post" ]
}

@test "extract_date_slug: handles different years" {
    result=$(extract_date_slug "_posts/2024/2024-01-15-test.md")

    [ "$result" = "2024-01-15-test" ]
}

@test "extract_date_slug: handles long slugs" {
    result=$(extract_date_slug "_posts/2025/2025-11-21-this-is-a-very-long-slug-name.md")

    [ "$result" = "2025-11-21-this-is-a-very-long-slug-name" ]
}

@test "extract_date_slug: handles paths without year directory" {
    result=$(extract_date_slug "_posts/2025-11-21-my-post.md")

    [ "$result" = "2025-11-21-my-post" ]
}

# === Integration: Full workflow ===

@test "full workflow: parse placeholder and extract parts" {
    content="Here is [image1: A beautiful sunset over mountains] in the post"

    # Find placeholder
    placeholder=$(find_image_placeholders "$content")

    # Extract parts
    id=$(extract_placeholder_id "$placeholder")
    desc=$(extract_placeholder_description "$placeholder")

    [ "$id" = "image1" ]
    [ "$desc" = "A beautiful sunset over mountains" ]
}

@test "full workflow: multiple placeholders preserve order" {
    content="[image1: First] then [image2: Second] then [image3: Third]"

    placeholders=$(find_image_placeholders "$content")

    first=$(echo "$placeholders" | head -1)
    second=$(echo "$placeholders" | head -2 | tail -1)
    third=$(echo "$placeholders" | tail -1)

    [ "$(extract_placeholder_id "$first")" = "image1" ]
    [ "$(extract_placeholder_id "$second")" = "image2" ]
    [ "$(extract_placeholder_id "$third")" = "image3" ]
}
