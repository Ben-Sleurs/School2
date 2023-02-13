---------------------------
-- Create schema objects --
---------------------------

PROMPT
PROMPT Specify password for app_data as parameter 1:
DEFINE pass_app_data = &1

CONNECT app_data/&pass_app_data

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
