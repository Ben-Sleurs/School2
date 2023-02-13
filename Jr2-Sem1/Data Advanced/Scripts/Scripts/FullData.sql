--------------------------------------------------------
--  File created - dinsdag-december-13-2022   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Package ADMIN_PKG
--------------------------------------------------------
  CREATE OR REPLACE PACKAGE "APP_ADMIN"."ADMIN_PKG" 
AS
  PROCEDURE update_job
    ( p_job_id IN jobs.job_id%TYPE,
      p_job_title IN jobs.job_title%TYPE := NULL,
      p_min_salary IN jobs.min_salary%TYPE := NULL,
      p_max_salary IN jobs.max_salary%TYPE := NULL );
  PROCEDURE add_job
    ( p_job_id IN jobs.job_id%TYPE,
      p_job_title IN jobs.job_title%TYPE,
      p_min_salary IN jobs.min_salary%TYPE,
      p_max_salary IN jobs.max_salary%TYPE );
  PROCEDURE update_department
    ( p_department_id IN departments.department_id%TYPE,
      p_department_name IN departments.department_name%TYPE := NULL,
      p_manager_id IN departments.manager_id%TYPE := NULL,
      p_update_manager_id IN BOOLEAN := FALSE );
  FUNCTION add_department
    ( p_department_name IN departments.department_name%TYPE,
      p_manager_id IN departments.manager_id%TYPE )
    RETURN departments.department_id%TYPE;
END admin_pkg;
/
  GRANT EXECUTE ON "APP_ADMIN"."ADMIN_PKG" TO "APP_ADMIN_USER";
--------------------------------------------------------
--  DDL for Package Body ADMIN_PKG
--------------------------------------------------------
  CREATE OR REPLACE PACKAGE BODY "APP_ADMIN"."ADMIN_PKG" 
AS
  PROCEDURE update_job
    ( p_job_id IN jobs.job_id%TYPE,
      p_job_title IN jobs.job_title%TYPE := NULL,
      p_min_salary IN jobs.min_salary%TYPE := NULL,
      p_max_salary IN jobs.max_salary%TYPE := NULL )
  IS
  BEGIN
    UPDATE jobs
    SET job_title = NVL( p_job_title, job_title ),
    min_salary = NVL( p_min_salary, min_salary ),
    max_salary = NVL( p_max_salary, max_salary )
    WHERE job_id = p_job_id;
  END update_job;
  PROCEDURE add_job
    ( p_job_id IN jobs.job_id%TYPE,
      p_job_title IN jobs.job_title%TYPE,
      p_min_salary IN jobs.min_salary%TYPE,
      p_max_salary IN jobs.max_salary%TYPE )
  IS
  BEGIN
  INSERT INTO jobs ( job_id, job_title, min_salary, max_salary )
  VALUES ( p_job_id, p_job_title, p_min_salary, p_max_salary );
  END add_job;
  PROCEDURE update_department
    ( p_department_id IN departments.department_id%TYPE,
      p_department_name IN departments.department_name%TYPE := NULL,
      p_manager_id IN departments.manager_id%TYPE := NULL,
      p_update_manager_id IN BOOLEAN := FALSE )
  IS
  BEGIN
    IF ( p_update_manager_id ) THEN
     UPDATE departments
     SET department_name = NVL( p_department_name, department_name ),
     manager_id = p_manager_id
     WHERE department_id = p_department_id;
    ELSE
     UPDATE departments
     SET department_name = NVL( p_department_name, department_name )
     WHERE department_id = p_department_id;
    END IF;
  END update_department;
  FUNCTION add_department
    ( p_department_name IN departments.department_name%TYPE,
      p_manager_id IN departments.manager_id%TYPE )
      RETURN departments.department_id%TYPE
  IS
   l_department_id departments.department_id%TYPE := departments_sequence.NEXTVAL;
  BEGIN
   INSERT INTO departments ( department_id, department_name, manager_id )
   VALUES ( l_department_id, p_department_name, p_manager_id );
   RETURN l_department_id;
  END add_department;
END admin_pkg;
/
  GRANT EXECUTE ON "APP_ADMIN"."ADMIN_PKG" TO "APP_ADMIN_USER";
--------------------------------------------------------
--  DDL for Synonymn DEPARTMENTS
--------------------------------------------------------
  CREATE OR REPLACE SYNONYM "APP_ADMIN"."DEPARTMENTS" FOR "APP_DATA"."DEPARTMENTS";
--------------------------------------------------------
--  DDL for Synonymn DEPARTMENTS_SEQUENCE
--------------------------------------------------------
  CREATE OR REPLACE SYNONYM "APP_ADMIN"."DEPARTMENTS_SEQUENCE" FOR "APP_DATA"."DEPARTMENTS#_SEQUENCE";
--------------------------------------------------------
--  DDL for Synonymn JOBS
--------------------------------------------------------
  CREATE OR REPLACE SYNONYM "APP_ADMIN"."JOBS" FOR "APP_DATA"."JOBS";
--------------------------------------------------------
--  File created - dinsdag-december-13-2022   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Synonymn EMPLOYEES_PKG
--------------------------------------------------------
  CREATE OR REPLACE SYNONYM "APP_ADMIN_USER"."EMPLOYEES_PKG" FOR "APP_CODE"."EMPLOYEES_PKG";
  GRANT EXECUTE ON "APP_CODE"."EMPLOYEES_PKG" TO "APP_USER";
  GRANT EXECUTE ON "APP_CODE"."EMPLOYEES_PKG" TO "APP_ADMIN_USER";
--------------------------------------------------------
--  DDL for Synonymn DEPARTMENTS
--------------------------------------------------------
  CREATE OR REPLACE SYNONYM "APP_CODE"."DEPARTMENTS" FOR "APP_DATA"."DEPARTMENTS";
--------------------------------------------------------
--  DDL for Synonymn EMPLOYEES
--------------------------------------------------------
  CREATE OR REPLACE SYNONYM "APP_CODE"."EMPLOYEES" FOR "APP_DATA"."EMPLOYEES";
--------------------------------------------------------
--  DDL for Synonymn JOB_HISTORY
--------------------------------------------------------
  CREATE OR REPLACE SYNONYM "APP_CODE"."JOB_HISTORY" FOR "APP_DATA"."JOB_HISTORY";
--------------------------------------------------------
--  DDL for Synonymn JOBS
--------------------------------------------------------
  CREATE OR REPLACE SYNONYM "APP_CODE"."JOBS" FOR "APP_DATA"."JOBS";
--------------------------------------------------------
--------------------------------------------------------
--  File created - dinsdag-december-13-2022   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Package EMPLOYEES_PKG
--------------------------------------------------------
DROP PACKAGE "APP_CODE"."EMPLOYEES_PKG";

  CREATE OR REPLACE PACKAGE "APP_CODE"."EMPLOYEES_PKG" 
AS
  PROCEDURE get_employees_in_dept
    ( p_deptno IN employees.department_id%TYPE,
      p_result_set IN OUT SYS_REFCURSOR );
  PROCEDURE get_job_history
    ( p_employee_id IN employees.department_id%TYPE,
      p_result_set IN OUT SYS_REFCURSOR );
  PROCEDURE show_employee
    ( p_employee_id IN employees.employee_id%TYPE,
      p_result_set IN OUT SYS_REFCURSOR );
  PROCEDURE UPDATE_SALARY
    ( p_employee_id IN employees.employee_id%TYPE,
      p_new_salary IN employees.salary%TYPE);
  PROCEDURE CHANGE_JOB
    ( p_employee_id IN employees.employee_id%TYPE,
      p_new_job IN employees.job_id%TYPE,
      p_new_salary IN employees.salary%TYPE := NULL,
      p_new_dept IN employees.department_id%TYPE := NULL );
END employees_pkg;
/
  GRANT EXECUTE ON "APP_CODE"."EMPLOYEES_PKG" TO "APP_USER";
  GRANT EXECUTE ON "APP_CODE"."EMPLOYEES_PKG" TO "APP_ADMIN_USER";
--------------------------------------------------------
--  DDL for Package Body EMPLOYEES_PKG
--------------------------------------------------------
  CREATE OR REPLACE PACKAGE BODY "APP_CODE"."EMPLOYEES_PKG" 
AS
  PROCEDURE get_employees_in_dept
    ( p_deptno IN employees.department_id%TYPE,
      p_result_set IN OUT SYS_REFCURSOR )
  IS
    l_cursor SYS_REFCURSOR;
  BEGIN
    OPEN p_result_set FOR
      SELECT e.employee_id,
        e.first_name || ' ' || e.last_name name,
        TO_CHAR( e.hire_date, 'Dy Mon ddth, yyyy' ) hire_date,
        j.job_title,
        m.first_name || ' ' || m.last_name manager,
        d.department_name
      FROM employees e INNER JOIN jobs j ON (e.job_id = j.job_id)
        LEFT OUTER JOIN employees m ON (e.manager_id = m.employee_id)
        INNER JOIN departments d ON (e.department_id = d.department_id)
      WHERE e.department_id = p_deptno ;
  END get_employees_in_dept;

  PROCEDURE get_job_history
    ( p_employee_id IN employees.department_id%TYPE,
      p_result_set IN OUT SYS_REFCURSOR )
  IS
  BEGIN
    OPEN p_result_set FOR
      SELECT e.First_name || ' ' || e.last_name name, j.job_title,
        e.job_start_date start_date,
        TO_DATE(NULL) end_date
      FROM employees e INNER JOIN jobs j ON (e.job_id = j.job_id)
      WHERE e.employee_id = p_employee_id
      UNION ALL
      SELECT e.First_name || ' ' || e.last_name name,
        j.job_title,
        jh.start_date,
        jh.end_date
      FROM employees e INNER JOIN job_history jh
        ON (e.employee_id = jh.employee_id)
        INNER JOIN jobs j ON (jh.job_id = j.job_id)
      WHERE e.employee_id = p_employee_id
      ORDER BY start_date DESC;
  END get_job_history;
  PROCEDURE show_employee
    ( p_employee_id IN employees.employee_id%TYPE,
      p_result_set IN OUT sys_refcursor )
  IS
  BEGIN
    OPEN p_result_set FOR
      SELECT *
      FROM (SELECT TO_CHAR(e.employee_id) employee_id,
              e.first_name || ' ' || e.last_name name,
              e.email_addr,
              TO_CHAR(e.hire_date,'dd-mon-yyyy') hire_date,
              e.country_code,
              e.phone_number,
              j.job_title,
              TO_CHAR(e.job_start_date,'dd-mon-yyyy') job_start_date,
              to_char(e.salary) salary,
              m.first_name || ' ' || m.last_name manager,
              d.department_name
            FROM employees e INNER JOIN jobs j on (e.job_id = j.job_id)
              RIGHT OUTER JOIN employees m ON (m.employee_id = e.manager_id)
              INNER JOIN departments d ON (e.department_id = d.department_id)
            WHERE e.employee_id = p_employee_id)
      UNPIVOT (VALUE FOR ATTRIBUTE IN (employee_id, name, email_addr, hire_date,
        country_code, phone_number, job_title, job_start_date, salary, manager,
        department_name) );
  END show_employee;
  PROCEDURE UPDATE_SALARY
    ( p_employee_id IN employees.employee_id%TYPE,
      p_new_salary IN employees.salary%TYPE)
  IS
  BEGIN
    update employees
    set salary=p_new_salary
    where employee_id = p_employee_id;
  END UPDATE_SALARY;
  
  PROCEDURE CHANGE_JOB
    ( p_employee_id IN employees.employee_id%TYPE,
      p_new_job IN employees.job_id%TYPE,
      p_new_salary IN employees.salary%TYPE := NULL,
      p_new_dept IN employees.department_id%TYPE := NULL )
  IS
  BEGIN
    INSERT INTO job_history (employee_id, start_date, end_date, job_id,
    department_id)
    SELECT employee_id, job_start_date, TRUNC(SYSDATE), job_id, department_id
    FROM employees
    WHERE employee_id = p_employee_id;
    UPDATE employees
    SET job_id = p_new_job,
    department_id = NVL( p_new_dept, department_id ),
    salary = NVL( p_new_salary, salary ),
    job_start_date = TRUNC(SYSDATE)
    WHERE employee_id = p_employee_id;
  END change_job;
END employees_pkg;
/
  
--  File created - dinsdag-december-13-2022   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Sequence DEPARTMENTS#_SEQUENCE
--------------------------------------------------------
  DROP SEQUENCE "APP_DATA"."DEPARTMENTS#_SEQUENCE";
   CREATE SEQUENCE  "APP_DATA"."DEPARTMENTS#_SEQUENCE"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 10 START WITH 280 CACHE 20 NOORDER  NOCYCLE ;
  GRANT SELECT ON "APP_DATA"."DEPARTMENTS#_SEQUENCE" TO "APP_ADMIN";
--------------------------------------------------------
--  DDL for Sequence EMPLOYEES#_SEQUENCE
--------------------------------------------------------
   DROP SEQUENCE "APP_DATA"."EMPLOYEES#_SEQUENCE";
   CREATE SEQUENCE  "APP_DATA"."EMPLOYEES#_SEQUENCE"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 207 CACHE 20 NOORDER  NOCYCLE ;
  GRANT SELECT ON "APP_DATA"."EMPLOYEES#_SEQUENCE" TO "APP_CODE";
  GRANT SELECT ON "APP_DATA"."EMPLOYEES#_SEQUENCE" TO "APP_ADMIN";
--------------------------------------------------------
--  DDL for Table DEPARTMENTS#
--------------------------------------------------------
drop table "APP_DATA"."DEPARTMENTS#" cascade constraints;
  CREATE TABLE "APP_DATA"."DEPARTMENTS#" 
   (	"DEPARTMENT_ID" NUMBER(4,0), 
	"DEPARTMENT_NAME" VARCHAR2(30 BYTE), 
	"MANAGER_ID" NUMBER(6,0)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
  --------------------------------------------------------
--  DDL for Table JOB_HISTORY#
--------------------------------------------------------
 drop table "APP_DATA"."JOB_HISTORY#" cascade constraints;
  CREATE TABLE "APP_DATA"."JOB_HISTORY#" 
   (	"EMPLOYEE_ID" NUMBER(6,0), 
	"JOB_ID" VARCHAR2(10 BYTE), 
	"START_DATE" DATE, 
	"END_DATE" DATE, 
	"DEPARTMENT_ID" NUMBER(4,0)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
  --------------------------------------------------------
--  DDL for Table JOBS#
--------------------------------------------------------
 drop table "APP_DATA"."JOBS#" cascade constraints;
  CREATE TABLE "APP_DATA"."JOBS#" 
   (	"JOB_ID" VARCHAR2(10 BYTE), 
	"JOB_TITLE" VARCHAR2(35 BYTE), 
	"MIN_SALARY" NUMBER(6,0), 
	"MAX_SALARY" NUMBER(6,0)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Table EMPLOYEES#
--------------------------------------------------------
 drop table "APP_DATA"."EMPLOYEES#" cascade constraints;
  CREATE TABLE "APP_DATA"."EMPLOYEES#" 
   (	"EMPLOYEE_ID" NUMBER(6,0), 
	"FIRST_NAME" VARCHAR2(20 BYTE), 
	"LAST_NAME" VARCHAR2(25 BYTE), 
	"EMAIL_ADDR" VARCHAR2(25 BYTE), 
	"HIRE_DATE" DATE DEFAULT TRUNC(SYSDATE), 
	"COUNTRY_CODE" VARCHAR2(5 BYTE), 
	"PHONE_NUMBER" VARCHAR2(20 BYTE), 
	"JOB_ID" VARCHAR2(10 BYTE), 
	"JOB_START_DATE" DATE, 
	"SALARY" NUMBER(6,0), 
	"MANAGER_ID" NUMBER(6,0), 
	"DEPARTMENT_ID" NUMBER(4,0)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;


--------------------------------------------------------
--  DDL for View DEPARTMENTS
--------------------------------------------------------
  CREATE OR REPLACE FORCE VIEW "APP_DATA"."DEPARTMENTS" ("DEPARTMENT_ID", "DEPARTMENT_NAME", "MANAGER_ID") AS 
  SELECT "DEPARTMENT_ID","DEPARTMENT_NAME","MANAGER_ID" FROM DEPARTMENTS#;
  GRANT SELECT ON "APP_DATA"."DEPARTMENTS" TO "APP_CODE";
  GRANT DELETE, INSERT, SELECT, UPDATE ON "APP_DATA"."DEPARTMENTS" TO "APP_ADMIN";
  GRANT SELECT ON "APP_DATA"."DEPARTMENTS" TO "APP_ADMIN_USER";
--------------------------------------------------------
--  DDL for View EMPLOYEES
--------------------------------------------------------
  CREATE OR REPLACE FORCE VIEW "APP_DATA"."EMPLOYEES" ("EMPLOYEE_ID", "FIRST_NAME", "LAST_NAME", "EMAIL_ADDR", "HIRE_DATE", "COUNTRY_CODE", "PHONE_NUMBER", "JOB_ID", "JOB_START_DATE", "SALARY", "MANAGER_ID", "DEPARTMENT_ID") AS 
  SELECT "EMPLOYEE_ID","FIRST_NAME","LAST_NAME","EMAIL_ADDR","HIRE_DATE","COUNTRY_CODE","PHONE_NUMBER","JOB_ID","JOB_START_DATE","SALARY","MANAGER_ID","DEPARTMENT_ID" FROM employees#;
  GRANT DELETE, INSERT, SELECT, UPDATE ON "APP_DATA"."EMPLOYEES" TO "APP_CODE";
--------------------------------------------------------
--  DDL for View JOB_HISTORY
--------------------------------------------------------
  CREATE OR REPLACE FORCE VIEW "APP_DATA"."JOB_HISTORY" ("EMPLOYEE_ID", "JOB_ID", "START_DATE", "END_DATE", "DEPARTMENT_ID") AS 
  SELECT "EMPLOYEE_ID","JOB_ID","START_DATE","END_DATE","DEPARTMENT_ID"
FROM JOB_HISTORY#;
  GRANT INSERT, SELECT ON "APP_DATA"."JOB_HISTORY" TO "APP_CODE";
--------------------------------------------------------
--  DDL for View JOBS
--------------------------------------------------------
  CREATE OR REPLACE FORCE VIEW "APP_DATA"."JOBS" ("JOB_ID", "JOB_TITLE", "MIN_SALARY", "MAX_SALARY") AS 
  SELECT "JOB_ID","JOB_TITLE","MIN_SALARY","MAX_SALARY" from JOBS#;
  GRANT SELECT ON "APP_DATA"."JOBS" TO "APP_CODE";
  GRANT DELETE, INSERT, SELECT, UPDATE ON "APP_DATA"."JOBS" TO "APP_ADMIN";
  GRANT SELECT ON "APP_DATA"."JOBS" TO "APP_ADMIN_USER";


INSERT INTO APP_DATA.jobs SELECT * FROM hr.jobs;

alter table app_data.Departments# drop constraint DEPT#_DEPT_NOT_NULL;
alter table app_data.Departments# drop constraint DEPARTMENTS#_DM_NOT_NULL;
alter table "APP_DATA"."DEPARTMENTS#" drop constraint "DEPT#_PK";
truncate table "APP_DATA"."DEPARTMENTS#";

INSERT INTO APP_DATA.departments SELECT department_id, department_name, manager_id FROM hr.departments;
INSERT INTO APP_DATA.job_history SELECT employee_id, job_id, start_date, end_date, department_id FROM hr.job_history;
---------------
-- Load data --
---------------
INSERT INTO APP_DATA.employees(employee_id, first_name, last_name, email_addr,
 hire_date, country_code, phone_number, job_id, job_start_date, salary,
 manager_id, department_id)
SELECT employee_id test, first_name, last_name, email, hire_date,
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
 WHERE jh.employee_id = hr_employees.employee_id), hire_date),
salary, manager_id, department_id
FROM HR.EMPLOYEES hr_employees;
COMMIT;
--------------------------------------------------------
--  DDL for Index DEPARTMENTS#_DM_NOT_NULL
--------------------------------------------------------
 drop index "APP_DATA"."DEPARTMENTS#_DM_NOT_NULL";
  CREATE UNIQUE INDEX "APP_DATA"."DEPARTMENTS#_DM_NOT_NULL" ON "APP_DATA"."DEPARTMENTS#" ("DEPARTMENT_NAME") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index DEPARTMENTS#_PK
--------------------------------------------------------
drop index "APP_DATA"."DEPARTMENTS#_PK";
  CREATE UNIQUE INDEX "APP_DATA"."DEPARTMENTS#_PK" ON "APP_DATA"."DEPARTMENTS#" ("DEPARTMENT_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index EMPLOYEES#_PK
--------------------------------------------------------

drop index "APP_DATA"."EMPLOYEES#_PK";
  CREATE UNIQUE INDEX "APP_DATA"."EMPLOYEES#_PK" ON "APP_DATA"."EMPLOYEES#" ("EMPLOYEE_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index JOB_HISTORY#_PK
--------------------------------------------------------
drop index "APP_DATA"."JOB_HISTORY#_PK";
  CREATE UNIQUE INDEX "APP_DATA"."JOB_HISTORY#_PK" ON "APP_DATA"."JOB_HISTORY#" ("EMPLOYEE_ID", "START_DATE") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index JOBS#_PK
--------------------------------------------------------
drop index "APP_DATA"."JOBS#_PK";
  CREATE UNIQUE INDEX "APP_DATA"."JOBS#_PK" ON "APP_DATA"."JOBS#" ("JOB_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index DEPARTMENTS#_PK
--------------------------------------------------------
drop index "APP_DATA"."DEPARTMENTS#_PK";
  CREATE UNIQUE INDEX "APP_DATA"."DEPARTMENTS#_PK" ON "APP_DATA"."DEPARTMENTS#" ("DEPARTMENT_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index DEPARTMENTS#_DM_NOT_NULL
--------------------------------------------------------
drop index "APP_DATA"."DEPARTMENTS#_DM_NOT_NULL";
  CREATE UNIQUE INDEX "APP_DATA"."DEPARTMENTS#_DM_NOT_NULL" ON "APP_DATA"."DEPARTMENTS#" ("DEPARTMENT_NAME") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index EMPLOYEES#_PK
--------------------------------------------------------
drop index "APP_DATA"."EMPLOYEES#_PK";
  CREATE UNIQUE INDEX "APP_DATA"."EMPLOYEES#_PK" ON "APP_DATA"."EMPLOYEES#" ("EMPLOYEE_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index JOB_HISTORY#_PK
--------------------------------------------------------
drop index "APP_DATA"."JOB_HISTORY#_PK";
  CREATE UNIQUE INDEX "APP_DATA"."JOB_HISTORY#_PK" ON "APP_DATA"."JOB_HISTORY#" ("EMPLOYEE_ID", "START_DATE") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index JOBS#_PK
--------------------------------------------------------

drop index "APP_DATA"."JOBS#_PK";
  CREATE UNIQUE INDEX "APP_DATA"."JOBS#_PK" ON "APP_DATA"."JOBS#" ("JOB_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;

--------------------------------------------------------
--  DDL for Trigger EMPLOYEES#_AIUFER
--------------------------------------------------------
  CREATE OR REPLACE TRIGGER "APP_DATA"."EMPLOYEES#_AIUFER" 
AFTER INSERT OR UPDATE OF salary, job_id ON app_data.employees# FOR EACH ROW
DECLARE
 l_cnt NUMBER;
BEGIN
LOCK TABLE jobs IN SHARE MODE; -- Ensure that jobs does not change
                               -- during the following query.
     SELECT COUNT(*) INTO l_cnt
     FROM jobs
     WHERE job_id = :NEW.job_id
     AND :NEW.salary BETWEEN min_salary AND max_salary;

     IF (l_cnt<>1) THEN
       RAISE_APPLICATION_ERROR( -20002,
         CASE
           WHEN :new.job_id = :old.job_id
           THEN 'Salary modification invalid'
           ELSE 'Job reassignment puts salary out of range'
         END );
     END IF;
END;
/
ALTER TRIGGER "APP_DATA"."EMPLOYEES#_AIUFER" ENABLE;

--------------------------------------------------------
--  DDL for Trigger JOBS#_AUFER
--------------------------------------------------------
  CREATE OR REPLACE TRIGGER "APP_DATA"."JOBS#_AUFER" 
AFTER UPDATE OF min_salary, max_salary ON app_data.jobs# FOR EACH ROW
 WHEN (NEW.min_salary > OLD.min_salary OR NEW.max_salary < OLD.max_salary) DECLARE
 l_cnt NUMBER;
BEGIN
 LOCK TABLE employees# IN SHARE MODE;
 SELECT COUNT(*) INTO l_cnt
 FROM employees#
 WHERE job_id = :NEW.job_id
 AND salary NOT BETWEEN :NEW.min_salary and :NEW.max_salary;

 IF (l_cnt>0) THEN
 RAISE_APPLICATION_ERROR( -20001,
 'Salary update would violate ' || l_cnt || ' existing employee records' );
 END IF;
END;
/
ALTER TRIGGER "APP_DATA"."JOBS#_AUFER" ENABLE;
--------------------------------------------------------
--  DDL for Trigger EMPLOYEES#_AIUFER
--------------------------------------------------------
  CREATE OR REPLACE TRIGGER "APP_DATA"."EMPLOYEES#_AIUFER" 
AFTER INSERT OR UPDATE OF salary, job_id ON app_data.employees# FOR EACH ROW
DECLARE
 l_cnt NUMBER;
BEGIN
LOCK TABLE jobs IN SHARE MODE; -- Ensure that jobs does not change
                               -- during the following query.
     SELECT COUNT(*) INTO l_cnt
     FROM jobs
     WHERE job_id = :NEW.job_id
     AND :NEW.salary BETWEEN min_salary AND max_salary;

     IF (l_cnt<>1) THEN
       RAISE_APPLICATION_ERROR( -20002,
         CASE
           WHEN :new.job_id = :old.job_id
           THEN 'Salary modification invalid'
           ELSE 'Job reassignment puts salary out of range'
         END );
     END IF;
END;
/
ALTER TRIGGER "APP_DATA"."EMPLOYEES#_AIUFER" ENABLE;
--------------------------------------------------------
--  DDL for Trigger JOBS#_AUFER
--------------------------------------------------------
  CREATE OR REPLACE TRIGGER "APP_DATA"."JOBS#_AUFER" 
AFTER UPDATE OF min_salary, max_salary ON app_data.jobs# FOR EACH ROW
 WHEN (NEW.min_salary > OLD.min_salary OR NEW.max_salary < OLD.max_salary) DECLARE
 l_cnt NUMBER;
BEGIN
 LOCK TABLE employees# IN SHARE MODE;
 SELECT COUNT(*) INTO l_cnt
 FROM employees#
 WHERE job_id = :NEW.job_id
 AND salary NOT BETWEEN :NEW.min_salary and :NEW.max_salary;

 IF (l_cnt>0) THEN
 RAISE_APPLICATION_ERROR( -20001,
 'Salary update would violate ' || l_cnt || ' existing employee records' );
 END IF;
END;
/
ALTER TRIGGER "APP_DATA"."JOBS#_AUFER" ENABLE;
--------------------------------------------------------
--  Constraints for Table DEPARTMENTS#
--------------------------------------------------------
  ALTER TABLE "APP_DATA"."DEPARTMENTS#" ADD CONSTRAINT "DEPT#_DEPT_NOT_NULL" UNIQUE ("DEPARTMENT_NAME")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
  ALTER TABLE "APP_DATA"."DEPARTMENTS#" ADD CONSTRAINT "DEPT#_PK" PRIMARY KEY ("DEPARTMENT_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
  ALTER TABLE "APP_DATA"."DEPARTMENTS#" MODIFY ("DEPARTMENT_NAME" CONSTRAINT "DEPT#DEPT_NAME_NOT_NULL" NOT NULL ENABLE);
  ALTER TABLE "APP_DATA"."DEPARTMENTS#" MODIFY ("DEPARTMENT_ID" CONSTRAINT "DEPT#DEPT_ID_NOT_NULL" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table EMPLOYEES#
--------------------------------------------------------
  ALTER TABLE "APP_DATA"."EMPLOYEES#" ADD CONSTRAINT "EMP#_CHECK" CHECK (TRUNC(hire_date) = hire_date) ENABLE;
  ALTER TABLE "APP_DATA"."EMPLOYEES#" ADD CONSTRAINT "EMP#_PK" PRIMARY KEY ("EMPLOYEE_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
  ALTER TABLE "APP_DATA"."EMPLOYEES#" MODIFY ("SALARY" CONSTRAINT "EMP#_SALARY_NOT_NULL" NOT NULL ENABLE);
  ALTER TABLE "APP_DATA"."EMPLOYEES#" MODIFY ("JOB_START_DATE" CONSTRAINT "EMP#_JOB_START_DATE_NOT_NULL" NOT NULL ENABLE);
  ALTER TABLE "APP_DATA"."EMPLOYEES#" MODIFY ("JOB_ID" CONSTRAINT "EMP#_JOB_ID_NOT_NULL" NOT NULL ENABLE);
  ALTER TABLE "APP_DATA"."EMPLOYEES#" MODIFY ("PHONE_NUMBER" CONSTRAINT "EMP#_PHONE_NUMBER_NOT_NULL" NOT NULL ENABLE);
  ALTER TABLE "APP_DATA"."EMPLOYEES#" MODIFY ("COUNTRY_CODE" CONSTRAINT "EMP#_COUNTRY_CODE_NOT_NULL" NOT NULL ENABLE);
  ALTER TABLE "APP_DATA"."EMPLOYEES#" MODIFY ("HIRE_DATE" CONSTRAINT "EMP#_HIRE_DATE_NOT_NULL" NOT NULL ENABLE);
  ALTER TABLE "APP_DATA"."EMPLOYEES#" MODIFY ("EMAIL_ADDR" CONSTRAINT "EMP#_EMAIL_ADDR_NOT_NULL" NOT NULL ENABLE);
  ALTER TABLE "APP_DATA"."EMPLOYEES#" MODIFY ("LAST_NAME" CONSTRAINT "EMP#_LAST_NAME_NOT_NULL" NOT NULL ENABLE);
  ALTER TABLE "APP_DATA"."EMPLOYEES#" MODIFY ("FIRST_NAME" CONSTRAINT "EMP#_FIRST_NAME_NOT_NULL" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table JOB_HISTORY#
--------------------------------------------------------
  ALTER TABLE "APP_DATA"."JOB_HISTORY#" ADD CONSTRAINT "JH#_START_DATE_CHECK" CHECK (TRUNC(start_date) < end_date) ENABLE;
  ALTER TABLE "APP_DATA"."JOB_HISTORY#" ADD CONSTRAINT "JH#_END_DATE_NOT_NULL" CHECK ("END_DATE"IS NOT NULL) ENABLE;
  ALTER TABLE "APP_DATA"."JOB_HISTORY#" ADD CONSTRAINT "JH#_DM_ID_NOT_NULL" CHECK ("DEPARTMENT_ID"IS NOT NULL) ENABLE;
  ALTER TABLE "APP_DATA"."JOB_HISTORY#" ADD CONSTRAINT "JH#_PK" PRIMARY KEY ("EMPLOYEE_ID", "START_DATE")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
  ALTER TABLE "APP_DATA"."JOB_HISTORY#" MODIFY ("START_DATE" CONSTRAINT "JH#_START_DATE_NOT_NULL" NOT NULL ENABLE);
  ALTER TABLE "APP_DATA"."JOB_HISTORY#" MODIFY ("EMPLOYEE_ID" CONSTRAINT "JH#_EMPLOYEE_ID_PK" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table JOBS#
--------------------------------------------------------
  ALTER TABLE "APP_DATA"."JOBS#" ADD CONSTRAINT "JOBS#_PK" PRIMARY KEY ("JOB_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
  ALTER TABLE "APP_DATA"."JOBS#" ADD CONSTRAINT "JOBS#_MAX_SALARY_NOT_NULL" CHECK ("MAX_SALARY"IS NOT NULL) ENABLE;
  ALTER TABLE "APP_DATA"."JOBS#" ADD CONSTRAINT "JOBS#_MIN_SALARY_NOT_NULL" CHECK ("MIN_SALARY"IS NOT NULL) ENABLE;
  ALTER TABLE "APP_DATA"."JOBS#" ADD CONSTRAINT "JOBS#_JOB_TITLE_NOT_NULL" CHECK ("JOB_TITLE"IS NOT NULL) ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table DEPARTMENTS#
--------------------------------------------------------
  ALTER TABLE "APP_DATA"."DEPARTMENTS#" ADD CONSTRAINT "DEPT#_TO_EMPLOYEES#_FK" FOREIGN KEY ("MANAGER_ID")
	  REFERENCES "APP_DATA"."EMPLOYEES#" ("EMPLOYEE_ID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table EMPLOYEES#
--------------------------------------------------------
  ALTER TABLE "APP_DATA"."EMPLOYEES#" ADD CONSTRAINT "EMP#_DEPT#_FK" FOREIGN KEY ("DEPARTMENT_ID")
	  REFERENCES "APP_DATA"."DEPARTMENTS#" ("DEPARTMENT_ID") ENABLE;
  ALTER TABLE "APP_DATA"."EMPLOYEES#" ADD CONSTRAINT "EMP#_TO_EMP#_FK" FOREIGN KEY ("MANAGER_ID")
	  REFERENCES "APP_DATA"."EMPLOYEES#" ("EMPLOYEE_ID") ENABLE;
  ALTER TABLE "APP_DATA"."EMPLOYEES#" ADD CONSTRAINT "EMP#_TO_JOBS#_FK" FOREIGN KEY ("JOB_ID")
	  REFERENCES "APP_DATA"."JOBS#" ("JOB_ID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table JOB_HISTORY#
--------------------------------------------------------
  ALTER TABLE "APP_DATA"."JOB_HISTORY#" ADD CONSTRAINT "JH#_TO_DEPT#_FK" FOREIGN KEY ("DEPARTMENT_ID")
	  REFERENCES "APP_DATA"."DEPARTMENTS#" ("DEPARTMENT_ID") ENABLE;
  ALTER TABLE "APP_DATA"."JOB_HISTORY#" ADD CONSTRAINT "JH#_TO_JOBS#_FK" FOREIGN KEY ("JOB_ID")
	  REFERENCES "APP_DATA"."JOBS#" ("JOB_ID") ENABLE;
--------------------------------------------------------
--  File created - dinsdag-december-13-2022   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Synonymn EMPLOYEES_PKG
--------------------------------------------------------
  CREATE OR REPLACE SYNONYM "APP_USER"."EMPLOYEES_PKG" FOR "APP_CODE"."EMPLOYEES_PKG";
