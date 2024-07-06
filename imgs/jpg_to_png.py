import os
import glob
from PIL import Image

def convert_jpg_to_png(folder_path):
    for subdir, dirs, files in os.walk(folder_path):
        for dir in dirs:
            dir_path = os.path.join(subdir, dir)
            for file in os.listdir(dir_path):
                if file.endswith('.jpg'):
                    print(f"Converting {file} to PNG...")
                    jpg_path = os.path.join(dir_path, file)
                    png_path = os.path.join(dir_path, file.replace('.jpg', '.png'))
                    img = Image.open(jpg_path)
                    img.save(png_path)
                    os.remove(jpg_path)

folder_path = './'
convert_jpg_to_png(folder_path)
