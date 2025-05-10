#!/bin/bash

# Initialize counter
COUNTER=1

# Expand home directory and use argument
FOLDER_PATH="$HOME/Projects/e-commerce-website/wwwroot/images/products/$1"

# Check if folder exists
if [ ! -d "$FOLDER_PATH" ]; then
  echo "Folder does not exist: $FOLDER_PATH"
  exit 1
fi

# Loop through files and rename
for file in "$FOLDER_PATH"/*; do
  if [ -f "$file" ]; then
    extension="${file##*.}"
    filename="$1-$COUNTER.$extension"
    mv "$file" "$FOLDER_PATH/$filename"
    echo "Renamed: $filename"
    COUNTER=$((COUNTER + 1))
  fi
done
