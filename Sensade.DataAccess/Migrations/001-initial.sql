-- create tables for initial model classes

-- create type for status attribute in parking_space
CREATE TYPE status AS ENUM('FREE', 'UNAVAILABLE');

-- create parking_area table
CREATE TABLE parking_area (
	id serial PRIMARY KEY,
	street_address VARCHAR(255) NOT NULL,
	city VARCHAR(255) NOT NULL,
	zip_code VARCHAR(255) NOT NULL,
	latitude NUMERIC NOT NULL,
	longitude NUMERIC NOT NULL
);

-- create parking_space table
CREATE TABLE parking_space(
	id serial PRIMARY KEY,
	status status DEFAULT 'FREE',
	space_no INT NOT NULL,
	parking_area_id INT,
	CONSTRAINT fk_parking_area FOREIGN KEY(parking_area_id) REFERENCES parking_area(id)
);
