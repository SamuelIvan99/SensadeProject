-- insert mock parking areas and spaces into database

INSERT INTO parking_area (street_address, city, zip_code, latitude, longitude)
VALUES ('633 5th Crossing', 'Osiek', '87-340', 53.1595566, 19.3969614),
('57458 Boyd Trail', 'Thung Song', '80110', 8.1642301, 99.6756794),
('17321 Dunning Center', 'Nyköping', '611 35', 58.7472646, 17.0266546);

INSERT INTO parking_space(status, space_no, parking_area_id)
VALUES ('FREE', 73, 1),
('FREE', 82, 2),
('OCCUPIED', 8, 2),
('OCCUPIED', 55, 1),
('OCCUPIED', 67, 1),
('OCCUPIED', 99, 1),
('OCCUPIED', 26, 2),
('OCCUPIED', 97, 3),
('FREE', 18, 1),
('OCCUPIED', 18, 2),
('FREE', 25, 1),
('FREE', 37, 2),
('FREE', 36, 2),
('OCCUPIED', 66, 1),
('OCCUPIED', 20, 2),
('FREE', 49, 1),
('FREE', 45, 3),
('OCCUPIED', 59, 2),
('FREE', 24, 2),
('OCCUPIED', 54, 3);
