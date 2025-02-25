# Use the official MongoDB image from Docker Hub
FROM docker.io/library/mongo:4.4

# Set the working directory in the container
WORKDIR /usr/src/configs

# Copy the configuration file from the current directory to the working directory in the container
COPY mongod.conf .

# Ensure the MongoDB data and log directories exist and have the right permissions
RUN mkdir -p /var/lib/mongodb /var/log/mongodb && \
    chown -R mongodb:mongodb /var/lib/mongodb /var/log/mongodb

# Expose port 27017 to the outside world (default MongoDB port)
EXPOSE 27017

# Start MongoDB when the container launches with the configuration file
CMD ["mongod", "--config", "/usr/src/configs/mongod.conf"]