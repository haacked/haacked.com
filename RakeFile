require "html/proofer"

`chcp 65001`

task :test do
  sh "bundle exec jekyll build --trace"
  # ignore href="#" for the "Copy to clipboard" button
  HTML::Proofer.new("./_site", :href_ignore => ["#"]).run
end