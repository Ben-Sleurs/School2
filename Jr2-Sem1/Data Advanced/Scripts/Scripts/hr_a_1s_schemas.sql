--------------------
-- Create schemas --
--------------------

PROMPT
PROMPT Specify password for the HR application user (app_data) as parameter 1:
DEFINE pass_hr_a = &1
PROMPT
PROMPT Specify password for SYSTEM (DBA) as parameter 2:
DEFINE pass_system = &2

CONNECT system/&pass_system;

DROP USER app_data CASCADE;

CREATE USER app_data IDENTIFIED BY &pass_hr_a
DEFAULT TABLESPACE USERS
QUOTA UNLIMITED ON USERS;

---------------------------------
-- Grant privileges to schemas --
---------------------------------

GRANT CREATE SESSION TO app_data;

GRANT CREATE TABLE, CREATE VIEW, CREATE TRIGGER, CREATE SEQUENCE TO app_data;

GRANT SELECT ON HR.DEPARTMENTS TO app_data;

GRANT SELECT ON HR.EMPLOYEES TO app_data;

GRANT SELECT ON HR.JOB_HISTORY TO app_data;

GRANT SELECT ON HR.JOBS TO app_data;
