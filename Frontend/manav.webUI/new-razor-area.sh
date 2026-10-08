#!/bin/bash

PROJECT_ROOT="."
AREA=""

while getopts "r:a:" opt; do
  case "$opt" in
    r) PROJECT_ROOT="$OPTARG" ;;
    a) AREA="$OPTARG" ;;
    *)
      echo "Kullanım: ./new-razor-area.sh -r ProjeYolu -a AreaAdi"
      exit 1
      ;;
  esac
done

if [ -z "$AREA" ]; then
  echo "HATA: -a (Area adı) zorunludur."
  echo "Örnek: ./new-razor-area.sh -r . -a User"
  exit 1
fi

AREA_ROOT="$PROJECT_ROOT/Areas/$AREA"

mkdir -p "$AREA_ROOT/Controllers" \
         "$AREA_ROOT/ViewComponents" \
         "$AREA_ROOT/Models" \
         "$AREA_ROOT/Views"

echo "Area klasörleri oluşturuldu: $AREA_ROOT"


#    chmod +x new-razor-area.sh
#    ./new-razor-area.sh -r . -a User