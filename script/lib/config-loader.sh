#!/usr/bin/env bash
# Safe configuration loader for blog workflow scripts
# Prevents command injection by parsing config as data, not code

load_blog_config() {
  local config_file="$1"
  local required="${2:-true}"

  if [[ ! -f "$config_file" ]]; then
    if [[ "$required" == "true" ]]; then
      echo "Error: Configuration file not found at $config_file" >&2
      echo "Copy .blog-config.sh.example to .blog-config.sh and configure it." >&2
      return 1
    else
      return 0
    fi
  fi

  # Validate file permissions (should not be world-writable)
  if [[ -n "$(find "$config_file" -perm -002)" ]]; then
    echo "Warning: Config file is world-writable. Fix with: chmod 600 $config_file" >&2
  fi

  # Parse config safely - extract key=value pairs without executing
  while IFS= read -r line; do
    # Skip comments and empty lines
    [[ "$line" =~ ^[[:space:]]*# ]] && continue
    [[ -z "${line// }" ]] && continue

    # Extract key=value pairs
    if [[ "$line" =~ ^([A-Z_][A-Z0-9_]*)=\"([^\"]*)\"$ ]] || \
       [[ "$line" =~ ^([A-Z_][A-Z0-9_]*)=\'([^\']*)\'$ ]] || \
       [[ "$line" =~ ^([A-Z_][A-Z0-9_]*)=([^[:space:]]+)$ ]]; then
      local key="${BASH_REMATCH[1]}"
      local value="${BASH_REMATCH[2]}"

      # Only set recognized configuration variables
      case "$key" in
        OPENAI_API_KEY|TINYPNG_API_KEY|IMAGES_REPO_PATH|IMAGE_BASE_URL|\
        EDITOR|DALLE_MODEL|DALLE_SIZE|DALLE_QUALITY|DALLE_STYLE)
          # Expand $HOME in paths
          value="${value//\$HOME/$HOME}"
          value="${value//\~/$HOME}"

          # Export the variable
          export "$key=$value"
          ;;
        *)
          echo "Warning: Unknown configuration key '$key' in $config_file" >&2
          ;;
      esac
    fi
  done < "$config_file"

  return 0
}

validate_editor() {
  local editor="$1"

  # Allow empty editor
  [[ -z "$editor" ]] && return 0

  # Validate editor is a safe command (alphanumeric, dash, underscore, slash only)
  if [[ ! "$editor" =~ ^[a-zA-Z0-9/_-]+$ ]]; then
    echo "Warning: EDITOR contains potentially unsafe characters, ignoring" >&2
    return 1
  fi

  # Check if editor exists
  if ! command -v "$editor" &> /dev/null; then
    echo "Warning: Editor '$editor' not found in PATH" >&2
    return 1
  fi

  return 0
}

validate_path_safety() {
  local path="$1"
  local base_dir="$2"
  local description="${3:-path}"

  # Resolve to absolute path (handle both macOS and Linux)
  local real_path
  # Try GNU realpath -m first (handles non-existent paths), fall back to BSD realpath
  if ! real_path=$(realpath -m "$path" 2>/dev/null || realpath "$path" 2>/dev/null); then
    # If realpath fails, manually resolve the path
    # Convert to absolute path if relative
    if [[ "$path" = /* ]]; then
      real_path="$path"
    else
      real_path="$(pwd)/$path"
    fi
    # Normalize by removing ./ and collapsing ../ (loop until no more changes)
    real_path=$(echo "$real_path" | sed -E 's|/\./|/|g' | sed -E 's|^/\.\./|/|' | sed -E 's|/+|/|g')
    local prev_path=""
    while [[ "$real_path" != "$prev_path" ]]; do
      prev_path="$real_path"
      real_path=$(echo "$real_path" | sed -E 's|/[^/]+/\.\./|/|g')
    done
  fi

  # Ensure path is within base directory (prevent path traversal)
  local real_base
  # Resolve base directory (it should always exist)
  if ! real_base=$(realpath "$base_dir" 2>/dev/null); then
    echo "Error: Base directory does not exist: $base_dir" >&2
    return 1
  fi

  # Check for exact match or ensure it's a subdirectory with proper boundary
  # The regex ensures a trailing slash, so /blog won't match /blog-evil/
  if [[ "$real_path" != "$real_base"* ]] || \
     { [[ "$real_path" != "$real_base" ]] && [[ ! "$real_path" =~ ^"$real_base"/ ]]; }; then
    echo "Error: Path traversal detected in $description" >&2
    echo "  Path: $path" >&2
    echo "  Resolved: $real_path" >&2
    echo "  Expected under: $real_base" >&2
    return 1
  fi

  echo "$real_path"
  return 0
}

get_file_size() {
  local file_path="$1"
  # Cross-platform file size (macOS uses -f, Linux uses -c)
  stat -f%z "$file_path" 2>/dev/null || stat -c%s "$file_path" 2>/dev/null
}

extract_date_slug() {
  local post_path="$1"
  basename "$post_path" .md
}

format_size() {
  local bytes="$1"
  if (( bytes < 1024 )); then
    echo "${bytes}B"
  elif (( bytes < 1048576 )); then
    echo "$(( bytes / 1024 ))KB"
  else
    echo "$(( bytes / 1048576 ))MB"
  fi
}

check_required_dependencies() {
  local missing=()

  # Check for required commands
  local cmd
  for cmd in "$@"; do
    if ! command -v "$cmd" &> /dev/null; then
      missing+=("$cmd")
    fi
  done

  if [[ ${#missing[@]} -gt 0 ]]; then
    echo "Error: Missing required dependencies: ${missing[*]}" >&2
    echo "" >&2
    echo "Install with:" >&2
    for cmd in "${missing[@]}"; do
      case "$cmd" in
        jq)
          echo "  brew install jq  # macOS" >&2
          echo "  apt-get install jq  # Linux" >&2
          ;;
        curl)
          echo "  brew install curl  # macOS" >&2
          echo "  apt-get install curl  # Linux" >&2
          ;;
        magick)
          echo "  brew install imagemagick  # macOS" >&2
          echo "  apt-get install imagemagick  # Linux" >&2
          ;;
        git)
          echo "  brew install git  # macOS" >&2
          echo "  apt-get install git  # Linux" >&2
          ;;
      esac
    done
    return 1
  fi

  return 0
}
