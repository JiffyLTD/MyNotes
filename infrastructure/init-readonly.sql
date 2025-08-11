CREATE USER readonly_user WITH PASSWORD '12345678';

GRANT CONNECT ON DATABASE "mynotes-db" TO readonly_user;

GRANT USAGE ON SCHEMA note_service TO readonly_user;
GRANT USAGE ON SCHEMA favorite_note_service TO readonly_user;

GRANT SELECT ON ALL TABLES IN SCHEMA note_service TO readonly_user;
GRANT SELECT ON ALL TABLES IN SCHEMA favorite_note_service TO readonly_user;

ALTER DEFAULT PRIVILEGES IN SCHEMA note_service
GRANT SELECT ON TABLES TO readonly_user;

ALTER DEFAULT PRIVILEGES IN SCHEMA favorite_note_service
GRANT SELECT ON TABLES TO readonly_user;