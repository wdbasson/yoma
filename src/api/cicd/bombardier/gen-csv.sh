#!/bin/bash

# Generate CSV with X users
# format:
# email,password
# bombard-${count}@mail.com,qqqqqq

# Usage: gen-csv.sh <count> <register-users>
# Example: gen-csv.sh 100 true

set -x

COUNT=$1
REGISTER_USERS=$2

echo "email,password" > users.csv
for i in $(seq 0 $COUNT); do
  email="load-${i}@mail.com"

  # Register users
  # Will return 400 if user or phone number already exists.
  # Will probably be a good idea to write a script that deletes users.
  if [ "$REGISTER_USERS" == "true" ]; then
    phoneNumber=$(printf "%09d" $((0 + RANDOM % 999999999)))
    curl -X POST \
      https://api-load.stage.yoma.world/api/v1/auth/register \
      -H 'accept: text/plain' \
      -H 'Content-Type: application/json-patch+json' \
      -d "{
          \"firstName\": \"Test\",
          \"lastName\": \"User\",
          \"email\": \"${email}\",
          \"phoneNumber\": \"0${phoneNumber}\",
          \"countryofBirth\": \"SA\",
          \"countryofResidence\": \"SA\",
          \"gender\": 1,
          \"password\": \"qqqqqq\",
          \"confirmPassword\": \"qqqqqq\",
          \"isEmployer\": \"string\",
          \"ssoRedirectURL\": \"string\"
        }"
    sleep 1
  fi

  echo "${email},qqqqqq" >> users.csv
done
