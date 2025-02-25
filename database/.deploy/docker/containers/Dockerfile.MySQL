# Use the official image as a parent image
FROM docker.io/library/mysql:8.0

# Set the working directory in the container
WORKDIR /docker-entrypoint-initdb.d

# Add the files in your repository to the working directory
ADD . /docker-entrypoint-initdb.d

# Copy the database initialize script
COPY ./init.sql /docker-entrypoint-initdb.d

# Expose port 3306 to the outside world
EXPOSE 3306

# Run the command on container startup
CMD ["mysqld"]