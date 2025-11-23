# Blog Image Workflow

An automated workflow for creating, generating, and publishing blog post images with AI assistance.

## Overview

This workflow provides three scripts that streamline the process of adding images to blog posts:

1. **`script/new-post`** - Create a new blog post draft with image directory
2. **`script/generate-images`** - Interactively generate or select images using AI
3. **`script/publish-images`** - Optimize and publish images to the images repository

## Setup

### 1. Install Dependencies

The workflow scripts are written in bash and require the following command-line tools:

```bash
# macOS
brew install jq imagemagick

# Linux (Debian/Ubuntu)
sudo apt-get install jq imagemagick

# Linux (Fedora/RHEL)
sudo dnf install jq ImageMagick
```

**Required tools:**

- `jq` - JSON parsing for API responses
- `curl` - HTTP requests (usually pre-installed)
- `git` - Version control (usually pre-installed)

**Optional tools:**

- `imagemagick` (`magick` command) - Image optimization fallback when TinyPNG API is not available

### 2. Configure API Keys

Copy the example configuration:

```bash
cp .blog-config.sh.example .blog-config.sh
```

Edit `.blog-config.sh` and add your API keys:

```bash
OPENAI_API_KEY="sk-your-actual-api-key"
TINYPNG_API_KEY="your-tinypng-key"  # Optional
IMAGES_REPO_PATH="$HOME/dev/haacked/images"
IMAGE_BASE_URL="https://i.haacked.com"
```

**Getting API keys:**

- OpenAI: <https://platform.openai.com/api-keys>
- TinyPNG: <https://tinypng.com/developers> (optional, will use ImageMagick as fallback)

## Complete Workflow

### Step 1: Create a New Post

```bash
./script/new-post
```

You'll be prompted for:

- **Title**: The post title (e.g., "My Amazing Post")
- **Slug**: URL-friendly identifier (auto-generated if you press Enter)

This creates:

- `_posts/YYYY/YYYY-MM-DD-slug.md` - Your post file
- `.draft-images/YYYY-MM-DD-slug/` - Directory for draft images

### Step 2: Write Your Draft

Edit your post and add image placeholders:

```markdown
---
title: "My Amazing Post"
excerpt_image: ""  # Will be set after generating images
---

[image1: A vibrant sunset over mountains with a developer working on a laptop]

Here's the main content…

[image2: Screenshot of the application dashboard]

More content…

[image3: Architecture diagram showing microservices]
```

**Placeholder format:** `[imageN: description]`

- Use sequential numbers: `image1`, `image2`, etc.
- Descriptions are used as default AI prompts
- Add any existing images (screenshots, diagrams) to `.draft-images/YYYY-MM-DD-slug/`

### Step 3: Generate/Select Images Interactively

```bash
./script/generate-images _posts/YYYY/YYYY-MM-DD-slug.md
```

For each image placeholder, you'll be prompted to:

#### Option 1: Use an existing file

- Lists files in `.draft-images/YYYY-MM-DD-slug/`
- Select which file to use

#### Option 2: Generate with AI

- Edit the prompt (or press Enter to use default)
- Choose style: Vivid (dramatic) or Natural (subdued)
- Preview the generated image URL
- Approve or regenerate with modifications

The script:

- Generates/selects images
- Saves to `.draft-images/YYYY-MM-DD-slug/imageN.png`
- Updates your post with local references:

  ```markdown
  ![Description](./.draft-images/YYYY-MM-DD-slug/image1.png)
  ```

### Step 4: Preview Locally

```bash
jekyll serve
```

Visit <http://localhost:4000> to preview your post with local images.

### Step 5: Publish Images

When you're satisfied with all images:

```bash
./script/publish-images _posts/YYYY/YYYY-MM-DD-slug.md
```

This script:

1. **Optimizes** each image (using TinyPNG or ImageMagick)
2. **Copies** to `~/dev/haacked/images/blog/YYYY-MM-DD-slug/`
3. **Commits and pushes** to the images repository
4. **Updates** your post with final URLs:

   ```markdown
   ![Description](https://i.haacked.com/blog/YYYY-MM-DD-slug/image1.png)
   ```

### Step 6: Publish Your Post

```bash
# Preview with live URLs
jekyll serve

# Commit and create PR
git add _posts/YYYY/YYYY-MM-DD-slug.md
git commit -m "Add post about XYZ"
git push
gh pr create
```

## Tips and Tricks

### AI Image Generation

**Prompt tips:**

- Be specific about style, colors, mood
- Mention "minimalist", "photorealistic", "illustration", etc.
- Reference composition: "overhead view", "close-up", "wide angle"
- Example: "Minimalist illustration of a git tree with terminal windows as leaves, autumn colors, digital art style"

**Iterating:**

- If you don't like the result, choose "Modify prompt and regenerate"
- Try different styles (Vivid vs Natural)
- Save iterations by downloading multiple versions to `.draft-images/` first

### Manual Images

You can always add images manually:

1. Save to `.draft-images/YYYY-MM-DD-slug/`
2. When running `generate-images`, choose "Use existing file"
3. Continue with the publish workflow as normal

### Skipping Images

If you want to skip an image placeholder:

- In `generate-images`, choose "Skip this image"
- The placeholder remains in your post unchanged
- You can run the script again later to process it

### Re-running Scripts

All scripts are idempotent:

- `generate-images` - Will ask what to do with existing images
- `publish-images` - Will re-optimize and overwrite if needed

## Troubleshooting

### "Configuration file not found"

Make sure you've created `.blog-config.sh` from the example:

```bash
cp .blog-config.sh.example .blog-config.sh
```

### "Images repository not found"

Check that `images_repo_path` in `.blog-config.sh` points to your local clone of the images repository:

```bash
ls ~/dev/haacked/images  # Should exist
```

### TinyPNG API limit exceeded

The free tier has limits. The scripts will automatically fall back to ImageMagick. To use ImageMagick only, leave `tinypng_api_key` empty in your config.

### Git push fails

Make sure you have write access to the images repository and are authenticated:

```bash
cd ~/dev/haacked/images
git remote -v
git push  # Test push access
```

## File Structure

```text
haacked.com/
├── .blog-config.sh           # Your API keys (gitignored)
├── .blog-config.sh.example   # Template
├── .draft-images/            # Draft images (gitignored)
│   └── 2025-11-21-my-post/
│       ├── image1.png
│       ├── image2.png
│       └── screenshot.png
├── _posts/
│   └── 2025/
│       └── 2025-11-21-my-post.md
└── script/
    ├── new-post              # Create new post
    ├── generate-images       # Generate/select images
    └── publish-images        # Publish to images repo
```

## Advanced Usage

### Batch Processing

Process multiple posts:

```bash
for post in _posts/2025/*.md; do
  ./script/publish-images "$post"
done
```

### Custom DALL-E Settings

Edit `.blog-config.sh`:

```bash
DALLE_MODEL="dall-e-3"
DALLE_SIZE="1792x1024"      # Wide format
DALLE_QUALITY="hd"          # Higher quality
DALLE_STYLE="natural"       # Default style
```

### Auto-open in Editor

Set your preferred editor in `.blog-config.sh`:

```bash
EDITOR="code"  # VS Code
# EDITOR="vim"
# EDITOR="subl"
```

The `new-post` script will automatically open the post in your editor.

## Comparison with Old Workflow

| Step                    | Old Workflow                                  | New Workflow                    |
| ----------------------- | --------------------------------------------- | ------------------------------- |
| Create post             | Manual file creation                          | `./script/new-post`             |
| Generate image          | ChatGPT web → download                        | Interactive AI prompt           |
| Optimize                | Upload to tinypng.com → download              | Automatic                       |
| Add to images repo      | Manual copy to directory → commit → push      | Automatic                       |
| Update post URLs        | Manual URL construction and replacement       | Automatic                       |
| **Total manual steps**  | **8-10 steps per image**                      | **2 commands for all images**   |
| **Time per image**      | **~3-5 minutes**                              | **~1 minute**                   |

## Future Enhancements

Potential additions to the workflow:

- Support for other AI image providers (Midjourney, Stable Diffusion)
- Automatic alt text generation for accessibility
- Image format conversion (WEBP, AVIF)
- Bulk image operations
- Integration with screenshot tools
- Automated testing of image links
