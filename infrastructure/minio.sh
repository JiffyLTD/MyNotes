#!/bin/sh
set -e

minio server /data --console-address ':9001' & MINIO_PID=$!

sleep 5

mc alias set local http://localhost:9000 admin 12345678

mc mb local/default-images || true

mc anonymous set public local/default-images

mc cp --recursive /init-data/defaultCardImage.jpg local/default-images/

wait $MINIO_PID