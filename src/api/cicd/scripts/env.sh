# Script to locally set environment variables for local dev
export AWS_ACCESS_KEY_ID=$(aws configure get aws_access_key_id)
export AWS_SECRET_ACCESS_KEY=$(aws configure get aws_secret_access_key)

export ASPNETCORE_ENVIRONMENT=Local
export ASPNETCORE_URLS='http://+:5000'
