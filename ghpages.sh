#!/bin/sh
git checkout gh-pages && \
    cp * build/webgl/* -rvf . ; \
    git add Build* TemplateData* index.html && \
    git commit -m 'update' && \
    git push && \
    git checkout -
