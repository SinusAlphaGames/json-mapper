#!/bin/sh

set -e

MODEL="nomic-embed-text"

echo "Starting Ollama server..."

ollama serve &
OLLAMA_PID=$!

sleep 5

echo "Checking if model exists..."

if ollama list | grep -q "$MODEL"; then
  echo "Model already exists → skipping pull"
else
  echo "Model not found → pulling $MODEL"
  ollama pull $MODEL
fi

wait $OLLAMA_PID