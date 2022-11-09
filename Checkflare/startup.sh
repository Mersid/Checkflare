#!/usr/bin/env bash

/startup.sh &
xvfb-run /app/Checkflare --urls http://0.0.0.0:5000
