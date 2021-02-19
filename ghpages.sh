#!/bin/sh
git checkout gh-pages && cp * build/webgl/* -rvp . && git push 
