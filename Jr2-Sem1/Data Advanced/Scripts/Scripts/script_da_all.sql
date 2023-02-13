---------------------------------
-- OEFENING 1
---------------------------------

--------------------
--  Create schemas --
--------------------

PROMPT
PROMPT Specify password for the HR application user (app_data) as parameter 1:
DEFINE pass_hr_a = &1
PROMPT
PROMPT Specify password for SYSTEM (DBA) as parameter 2:
DEFINE pass_system = &2

CONNECT system/&pass_system;

DROP USER app_data CASCADE;
DROP USER app_code CASCADE;
DROP USER app_admin CASCADE;
DROP USER app_user CASCADE;
DROP USER app_admin_user CASCADE;

CREATE USER app_data IDENTIFIED BY &pass_hr_a
DEFAULT TABLESPACE USERS
QUOTA UNLIMITED ON USERS;

---------------------------------
-- Grant privileges to app_data --
---------------------------------

GRANT CREATE SESSION TO app_data;

GRANT CREATE TABLE, CREATE VIEW, CREATE TRIGGER, CREATE SEQUENCE TO app_data;

GRANT SELECT ON HR.DEPARTMENTS TO app_data;

GRANT SELECT ON HR.EMPLOYEES TO app_data;

GRANT SELECT ON HR.JOB_HISTORY TO app_data;

GRANT SELECT ON HR.JOBS TO app_data;

---------------------------------
-- Create User APP_CODE --
---------------------------------

CREATE USER app_code IDENTIFIED BY &pass_hr_a
DEFAULT TABLESPACE USERS
QUOTA UNLIMITED ON USERS;

---------------------------------
-- Grant privileges to APP_CODE --
---------------------------------

GRANT CREATE SESSION TO app_code;
GRANT CREATE PROCEDURE TO app_code;
GRANT CREATE SYNONYM TO app_code;

---------------------------------
-- Create User APP_ADMIN --
---------------------------------

CREATE USER app_admin IDENTIFIED BY &pass_hr_a
DEFAULT TABLESPACE USERS
QUOTA UNLIMITED ON USERS;

---------------------------------
-- Grant privileges to APP_ADMIN --
---------------------------------

GRANT CREATE SESSION TO app_admin;
GRANT CREATE PROCEDURE TO app_admin;
GRANT CREATE SYNONYM TO app_admin;


---------------------------------
-- Create User APP_USER --
---------------------------------

CREATE USER app_user IDENTIFIED BY &pass_hr_a
DEFAULT TABLESPACE USERS
QUOTA UNLIMITED ON USERS;

---------------------------------
-- Grant privileges to APP_USER --
---------------------------------

GRANT CREATE SESSION TO app_user;
GRANT CREATE SYNONYM TO app_user;

---------------------------------
-- Create User APP_ADMIN_USER --
---------------------------------

CREATE USER app_admin_user IDENTIFIED BY &pass_hr_a
DEFAULT TABLESPACE USERS
QUOTA UNLIMITED ON USERS;

---------------------------------
-- Grant privileges to APP_ADMIN_USER --
---------------------------------

GRANT CREATE SESSION TO app_admin_user;
GRANT CREATE SYNONYM TO app_admin_user;

---------------------------------
-- OEFENING 2
---------------------------------
---------------------------
-- Create schema objects --
---------------------------
CONNECT app_data/&pass_hr_a;

DROP TABLE employees#;

DROP VIEW employees;

CREATE TABLE employees#
(
employee_id NUMBER(6) CONSTRAINT employees#_pk PRIMARY KEY,
first_name VARCHAR2(20) CONSTRAINT emp#_first_name_not_null NOT NULL,
last_name VARCHAR2(25) CONSTRAINT emp#_last_name_not_null NOT NULL,
email_addr VARCHAR2(25) CONSTRAINT emp#_email_addr_not_null NOT NULL,
hire_date DATE DEFAULT TRUNC(SYSDATE) CONSTRAINT emp#_hire_date_not_null NOT NULL,
country_code VARCHAR2(5) CONSTRAINT emp#_country_code_not_null NOT NULL,
phone_number VARCHAR2(20) CONSTRAINT emp#_phone_number_not_null NOT NULL,
job_id VARCHAR2(10) CONSTRAINT emp#_job_id_not_null NOT NULL,
job_start_date DATE CONSTRAINT emp#_job_start_date_not_null NOT NULL,
salary NUMBER(6) CONSTRAINT emp#_salary_not_null NOT NULL,
manager_id NUMBER (6),
department_id NUMBER (4)
)
/

CREATE VIEW employees AS SELECT * FROM employees#
/
-- create jobs table and view---

DROP TABLE jobs#;

DROP VIEW jobs;

CREATE TABLE jobs#
(
job_id VARCHAR2(10) CONSTRAINT job#_pk PRIMARY KEY,
job_title VARCHAR2(35) CONSTRAINT job#_job_title_not_null NOT NULL,
min_salary NUMBER(6) CONSTRAINT job#_min_salary_not_null NOT NULL,
max_salary NUMBER(6) CONSTRAINT job#_max_salary_not_null NOT NULL
)
/

CREATE VIEW jobs AS SELECT * FROM jobs#
/

-- create department table and view---

DROP TABLE department#;

DROP VIEW department;

CREATE TABLE department#
(
department_id NUMBER(4) CONSTRAINT department#_pk PRIMARY KEY,
department_name VARCHAR2(30) CONSTRAINT department#_dep_name_NN NOT NULL UNIQUE,
manager_id NUMBER(6)
)
/

CREATE VIEW department AS SELECT * FROM department#
/

-- create job_history table and view---

DROP TABLE job_history#;

DROP VIEW job_history;

CREATE TABLE job_history#
(
employee_id NUMBER(6),
job_id VARCHAR2(10),
start_date DATE,
end_date DATE CONSTRAINT job_history#_end_date_NN NOT NULL,
department_id NUMBER(4) CONSTRAINT job_history#_dep_id_NN NOT NULL,
CONSTRAINT job_history#_pk PRIMARY KEY(employee_id, start_date)
)
/

CREATE VIEW job_history AS SELECT * FROM job_history#
/

---------------------------------
-- OEFENING 2B
---------------------------------

INSERT INTO jobs SELECT * FROM hr.jobs;
INSERT INTO department SELECT department_id, department_name, manager_id FROM hr.departments;
INSERT INTO job_history SELECT employee_id, job_id, start_date, end_date, department_id FROM hr.job_history;

---------------
-- Load data --
---------------

INSERT INTO employees (employee_id, first_name, last_name, email_addr,
 hire_date, country_code, phone_number, job_id, job_start_date, salary,
 manager_id, department_id)
SELECT employee_id, first_name, last_name, email, hire_date,
 CASE WHEN phone_number LIKE '011.%'
 THEN '+' || SUBSTR( phone_number, INSTR( phone_number, '.' )+1,
 INSTR( phone_number, '.', 1, 2 ) - INSTR( phone_number, '.' ) - 1 )
 ELSE '+1'
END country_code,
CASE WHEN phone_number LIKE '011.%'
 THEN SUBSTR( phone_number, INSTR(phone_number, '.', 1, 2 )+1 )
 ELSE phone_number
END phone_number,
job_id,
NVL( (SELECT MAX(end_date+1)
 FROM HR.JOB_HISTORY jh
 WHERE jh.employee_id = employees.employee_id), hire_date),
salary, manager_id, department_id
FROM HR.EMPLOYEES
/

COMMIT;

-------------------------------------------
--OEFENING 2C
-------------------------------------------

ALTER TABLE EMPLOYEES# ADD CONSTRAINT EMPLOYEES_MANAGERID_FK FOREIGN KEY ("MANAGER_ID")
	  REFERENCES EMPLOYEES# ("EMPLOYEE_ID") ENABLE;
ALTER TABLE EMPLOYEES# ADD CONSTRAINT EMPLOYEES_TO_DEPARTMENTS_FK FOREIGN KEY ("DEPARTMENT_ID")
	  REFERENCES DEPARTMENT# ("DEPARTMENT_ID") ENABLE;
ALTER TABLE EMPLOYEES# ADD CONSTRAINT EMPLOYEES_TO_JOBS_FK FOREIGN KEY ("JOB_ID")
	  REFERENCES JOBS# ("JOB_ID") ENABLE;

ALTER TABLE JOB_HISTORY# ADD CONSTRAINT JOB_HISTORY_TO_DEPARTMENTS_FK FOREIGN KEY ("DEPARTMENT_ID")
	  REFERENCES DEPARTMENT# ("DEPARTMENT_ID") ENABLE;
ALTER TABLE JOB_HISTORY# ADD CONSTRAINT JOB_HISTORY_TO_EMPLOYEES_FK FOREIGN KEY ("EMPLOYEE_ID")
	  REFERENCES EMPLOYEES# ("EMPLOYEE_ID") ENABLE;
ALTER TABLE JOB_HISTORY# ADD CONSTRAINT JOB_HISTORY_TO_JOBS_FK FOREIGN KEY ("JOB_ID")
	  REFERENCES JOBS# ("JOB_ID") ENABLE;


ALTER TABLE DEPARTMENT# ADD CONSTRAINT DEPARTMENT_TO_EMPLOYEES_FK FOREIGN KEY ("MANAGER_ID")
	  REFERENCES EMPLOYEES# ("EMPLOYEE_ID") ENABLE;


ALTER TABLE EMPLOYEES# ADD CONSTRAINT EMPLOYEES_JOB_START_DATE_CHECK CHECK (TRUNC(job_start_date) = job_start_date) ENABLE;
ALTER TABLE EMPLOYEES# ADD CONSTRAINT EMPLOYEES_HIRE_DATE_CHECK CHECK (TRUNC(hire_date) = hire_date) ENABLE;
-------------------------------------------
--OEFENING 2D
-------------------------------------------
CREATE SEQUENCE employees#_sequence INCREMENT BY 1 START
WITH 207;
CREATE SEQUENCE departments#_sequence INCREMENT BY 10 START
WITH 280;
-----------------------
-- TO BE ADDED LATER --
-----------------------

-- The following constraints will be added to the employees# table in the
-- script hr_a_2s_c_objects.sql:
-- job_id CONSTRAINT emp#_to_jobs_fk REFERENCES jobs#,
-- manager_id CONSTRAINT emp#_mgrid_to_emp_empid_fk REFERENCES employees#
-- department_id CONSTRAINT emp#_to_dept_fk REFERENCES departments#
-- hire_date CONSTRAINT emp#_hire_date_check CHECK(TRUNC(hire_date) = hire_date)
-- job_start_date CONSTRAINT emp#_job_start_date_check CHECK(TRUNC(JOB_START_DATE) =
-- job_start_date)