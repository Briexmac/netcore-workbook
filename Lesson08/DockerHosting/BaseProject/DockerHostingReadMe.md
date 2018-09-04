# Build Docker Container
Build the dockerfile into a container we can run
```
docker build -t aca/baseproject:lesson08 .
```

# Run Docker Container
```
docker run -e "MICROSOFT__KEY=<YourKeyForMicrosoftIdentity>" -e "MICROSOFT__SECRET=<YourSecretForMicrosoftIdentity>" -p 8000:80 -p 8443:443 aca/baseproject:lesson08
```