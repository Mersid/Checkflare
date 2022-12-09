#!/usr/bin/env bash

/startup.sh &
#/app/Checkflare --urls http://0.0.0.0:5000
xvfb-run /app/Checkflare --urls http://0.0.0.0:5000
