#!/bin/bash
# Bu script, Razor View klasor + .cshtml dosyasini otomatik olusturur.
# Kullanim:
#   ./new-razor-view.sh -a Admin -c Product -v Index -l "~/Areas/Admin/Views/AdminLayout/Index.cshtml"
#   ./new-razor-view.sh -c Home -v Index

# 1) Varsayilan degerler
PROJECT_ROOT="."
AREA=""
CONTROLLER=""
VIEW=""
LAYOUT=""
PARTIAL=false

# 2) Parametreleri oku (-a, -c, -v, -l, -p gibi kisaltmalarla girilecek)
while getopts "r:a:c:v:l:p" opt; do
  case $opt in
    r) PROJECT_ROOT="$OPTARG" ;;
    a) AREA="$OPTARG" ;;
    c) CONTROLLER="$OPTARG" ;;
    v) VIEW="$OPTARG" ;;
    l) LAYOUT="$OPTARG" ;;
    p) PARTIAL=true ;;
    *) echo "Bilinmeyen parametre"; exit 1 ;;
  esac
done

# 3) Zorunlu alanlari kontrol et
if [ -z "$CONTROLLER" ] || [ -z "$VIEW" ]; then
  echo "HATA: -c (Controller) ve -v (View) zorunludur."
  echo "Ornek: ./new-razor-view.sh -a Admin -c Product -v Index -l \"~/Areas/Admin/Views/AdminLayout/Index.cshtml\""
  exit 1
fi

# 4) Klasor yolunu belirle
if [ -n "$AREA" ]; then
  VIEW_FOLDER="$PROJECT_ROOT/Areas/$AREA/Views/$CONTROLLER"
else
  VIEW_FOLDER="$PROJECT_ROOT/Views/$CONTROLLER"
fi

# 5) Klasoru olustur (yoksa)
mkdir -p "$VIEW_FOLDER"
echo "Klasor hazir: $VIEW_FOLDER"

# 6) Dosya adini belirle (partial ise basina _ eklenir)
if [ "$PARTIAL" = true ]; then
  FILE_NAME="_${VIEW}.cshtml"
else
  FILE_NAME="${VIEW}.cshtml"
fi
FILE_PATH="$VIEW_FOLDER/$FILE_NAME"

# 7) Ayni isimde dosya var mi kontrol et
if [ -f "$FILE_PATH" ]; then
  echo "HATA: Dosya zaten var -> $FILE_PATH"
  exit 1
fi

# 8) Dosya icerigini olustur
{
  if [ "$PARTIAL" = false ]; then
    if [ -n "$LAYOUT" ]; then
      echo "@{"
      echo "    Layout = \"$LAYOUT\";"
      echo "}"
      echo ""
    fi
  fi
  echo "<h2>$VIEW</h2>"
} > "$FILE_PATH"

echo "View olusturuldu: $FILE_PATH"

# 9) Çalıştırma için dosya inzi al terminalde aşağıdaki kod ile 
#      chmod +x new-razor-view.sh   
#      ./new-razor-view.sh -a Admin -c ? -v Index -l "~/Areas/Admin/Views/AdminLayout/Index.cshtml"

##SignalR KOMUTU 
# dotnet tool install -g Microsoft.Web.LibraryManager.Cli
  #libman init -p unpkg
  #libman install @microsoft/signalr@6.0.6 -p unpkg -d wwwroot/lib/microsoft/signalr -f dist/browser/signalr.js dist/browser/signalr.min.js