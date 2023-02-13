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
--------------------------------------------------------
--  File created - dinsdag-december-13-2022   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Package EMPLOYEES_PKG
--------------------------------------------------------

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
      p_job_id IN employees.job_id%TYPE);
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
      p_job_id IN employees.job_id%TYPE)
  IS
  BEGIN
  insert into job_history
  values(p_employee_id, 
    update employees
    set job_id=p_job_id, start_date = trunc(sysdate)
    where employee_id = p_employee_id;
  END CHANGE_JOB;
END employees_pkg;

/

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
--  File created - dinsdag-december-13-2022   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Sequence DEPARTMENTS#_SEQUENCE
--------------------------------------------------------

   CREATE SEQUENCE  "APP_DATA"."DEPARTMENTS#_SEQUENCE"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 10 START WITH 280 CACHE 20 NOORDER  NOCYCLE ;
  GRANT SELECT ON "APP_DATA"."DEPARTMENTS#_SEQUENCE" TO "APP_ADMIN";
--------------------------------------------------------
--  DDL for Sequence EMPLOYEES#_SEQUENCE
--------------------------------------------------------

   CREATE SEQUENCE  "APP_DATA"."EMPLOYEES#_SEQUENCE"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 207 CACHE 20 NOORDER  NOCYCLE ;
  GRANT SELECT ON "APP_DATA"."EMPLOYEES#_SEQUENCE" TO "APP_CODE";
  GRANT SELECT ON "APP_DATA"."EMPLOYEES#_SEQUENCE" TO "APP_ADMIN";
--------------------------------------------------------
--  DDL for Table DEPARTMENTS#
--------------------------------------------------------

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
--  DDL for Table EMPLOYEES#
--------------------------------------------------------

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
--  DDL for Table JOB_HISTORY#
--------------------------------------------------------

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
--  DDL for View DEPARTMENTS
--------------------------------------------------------

  CREATE OR REPLACE FORCE VIEW "APP_DATA"."DEPARTMENTS" ("DEPARTMENT_ID", "DEPARTMENT_NAME", "MANAGER_ID") AS 
  SELECT "DEPARTMENT_ID","DEPARTMENT_NAME","MANAGER_ID"
    
FROM DEPARTMENTS#
;
  GRANT SELECT ON "APP_DATA"."DEPARTMENTS" TO "APP_CODE";
  GRANT DELETE, INSERT, SELECT, UPDATE ON "APP_DATA"."DEPARTMENTS" TO "APP_ADMIN";
  GRANT SELECT ON "APP_DATA"."DEPARTMENTS" TO "APP_ADMIN_USER";
--------------------------------------------------------
--  DDL for View EMPLOYEES
--------------------------------------------------------

  CREATE OR REPLACE FORCE VIEW "APP_DATA"."EMPLOYEES" ("EMPLOYEE_ID", "FIRST_NAME", "LAST_NAME", "EMAIL_ADDR", "HIRE_DATE", "COUNTRY_CODE", "PHONE_NUMBER", "JOB_ID", "JOB_START_DATE", "SALARY", "MANAGER_ID", "DEPARTMENT_ID") AS 
  SELECT "EMPLOYEE_ID","FIRST_NAME","LAST_NAME","EMAIL_ADDR","HIRE_DATE","COUNTRY_CODE","PHONE_NUMBER","JOB_ID","JOB_START_DATE","SALARY","MANAGER_ID","DEPARTMENT_ID" FROM employees#
;
  GRANT DELETE, INSERT, SELECT, UPDATE ON "APP_DATA"."EMPLOYEES" TO "APP_CODE";
--------------------------------------------------------
--  DDL for View JOB_HISTORY
--------------------------------------------------------

  CREATE OR REPLACE FORCE VIEW "APP_DATA"."JOB_HISTORY" ("EMPLOYEE_ID", "JOB_ID", "START_DATE", "END_DATE", "DEPARTMENT_ID") AS 
  SELECT "EMPLOYEE_ID","JOB_ID","START_DATE","END_DATE","DEPARTMENT_ID"
    
FROM JOB_HISTORY#
;
  GRANT INSERT, SELECT ON "APP_DATA"."JOB_HISTORY" TO "APP_CODE";
--------------------------------------------------------
--  DDL for View JOBS
--------------------------------------------------------

  CREATE OR REPLACE FORCE VIEW "APP_DATA"."JOBS" ("JOB_ID", "JOB_TITLE", "MIN_SALARY", "MAX_SALARY") AS 
  SELECT "JOB_ID","JOB_TITLE","MIN_SALARY","MAX_SALARY" from JOBS#
;
  GRANT SELECT ON "APP_DATA"."JOBS" TO "APP_CODE";
  GRANT DELETE, INSERT, SELECT, UPDATE ON "APP_DATA"."JOBS" TO "APP_ADMIN";
  GRANT SELECT ON "APP_DATA"."JOBS" TO "APP_ADMIN_USER";
REM INSERTING into APP_DATA.DEPARTMENTS#
SET DEFINE OFF;
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('10','Administration','200');
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('20','Marketing','201');
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('30','Purchasing','114');
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('40','Human Resources','203');
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('50','Shipping','121');
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('60','IT','103');
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('70','Public Relations','204');
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('80','Sales','145');
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('90','Executive','100');
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('100','Finance','108');
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('110','Accounting','205');
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('120','Treasury',null);
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('130','Corporate Tax',null);
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('140','Control And Credit',null);
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('150','Shareholder Services',null);
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('160','Benefits',null);
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('170','Manufacturing',null);
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('180','Construction',null);
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('190','Contracting',null);
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('200','Operations',null);
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('210','IT Support',null);
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('220','NOC',null);
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('230','IT Helpdesk',null);
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('240','Government Sales',null);
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('250','Retail Sales',null);
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('260','Recruiting',null);
Insert into APP_DATA.DEPARTMENTS# (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('270','Payroll',null);
REM INSERTING into APP_DATA.EMPLOYEES#
SET DEFINE OFF;
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('100','Steven','King','SKING',to_date('17/06/03','DD/MM/RR'),'+1','515.123.4567','AD_PRES',to_date('17/06/03','DD/MM/RR'),'24000',null,'90');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('101','Neena','Kochhar','NKOCHHAR',to_date('21/09/05','DD/MM/RR'),'+1','515.123.4568','AD_VP',to_date('16/03/05','DD/MM/RR'),'17000','100','90');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('102','Lex','De Haan','LDEHAAN',to_date('13/01/01','DD/MM/RR'),'+1','515.123.4569','AD_VP',to_date('25/07/06','DD/MM/RR'),'17000','100','90');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('103','Alexander','Hunold','AHUNOLD',to_date('03/01/06','DD/MM/RR'),'+1','590.423.4567','IT_PROG',to_date('03/01/06','DD/MM/RR'),'9000','102','60');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('104','Bruce','Ernst','BERNST',to_date('21/05/07','DD/MM/RR'),'+1','590.423.4568','IT_PROG',to_date('21/05/07','DD/MM/RR'),'6000','103','60');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('105','David','Austin','DAUSTIN',to_date('25/06/05','DD/MM/RR'),'+1','590.423.4569','IT_PROG',to_date('25/06/05','DD/MM/RR'),'4800','103','60');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('106','Valli','Pataballa','VPATABAL',to_date('05/02/06','DD/MM/RR'),'+1','590.423.4560','IT_PROG',to_date('05/02/06','DD/MM/RR'),'4800','103','60');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('107','Diana','Lorentz','DLORENTZ',to_date('07/02/07','DD/MM/RR'),'+1','590.423.5567','IT_PROG',to_date('07/02/07','DD/MM/RR'),'4200','103','60');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('108','Nancy','Greenberg','NGREENBE',to_date('17/08/02','DD/MM/RR'),'+1','515.124.4569','FI_MGR',to_date('17/08/02','DD/MM/RR'),'12008','101','100');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('109','Daniel','Faviet','DFAVIET',to_date('16/08/02','DD/MM/RR'),'+1','515.124.4169','FI_ACCOUNT',to_date('16/08/02','DD/MM/RR'),'9000','108','100');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('110','John','Chen','JCHEN',to_date('28/09/05','DD/MM/RR'),'+1','515.124.4269','FI_ACCOUNT',to_date('28/09/05','DD/MM/RR'),'8200','108','100');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('111','Ismael','Sciarra','ISCIARRA',to_date('30/09/05','DD/MM/RR'),'+1','515.124.4369','FI_ACCOUNT',to_date('30/09/05','DD/MM/RR'),'7700','108','100');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('112','Jose Manuel','Urman','JMURMAN',to_date('07/03/06','DD/MM/RR'),'+1','515.124.4469','FI_ACCOUNT',to_date('07/03/06','DD/MM/RR'),'7800','108','100');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('113','Luis','Popp','LPOPP',to_date('07/12/07','DD/MM/RR'),'+1','515.124.4567','FI_ACCOUNT',to_date('07/12/07','DD/MM/RR'),'6900','108','100');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('114','Den','Raphaely','DRAPHEAL',to_date('07/12/02','DD/MM/RR'),'+1','515.127.4561','PU_MAN',to_date('01/01/08','DD/MM/RR'),'11000','100','30');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('115','Alexander','Khoo','AKHOO',to_date('18/05/03','DD/MM/RR'),'+1','515.127.4562','PU_CLERK',to_date('18/05/03','DD/MM/RR'),'3100','114','30');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('116','Shelli','Baida','SBAIDA',to_date('24/12/05','DD/MM/RR'),'+1','515.127.4563','PU_CLERK',to_date('24/12/05','DD/MM/RR'),'2900','114','30');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('117','Sigal','Tobias','STOBIAS',to_date('24/07/05','DD/MM/RR'),'+1','515.127.4564','PU_CLERK',to_date('24/07/05','DD/MM/RR'),'2800','114','30');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('118','Guy','Himuro','GHIMURO',to_date('15/11/06','DD/MM/RR'),'+1','515.127.4565','PU_CLERK',to_date('15/11/06','DD/MM/RR'),'2600','114','30');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('119','Karen','Colmenares','KCOLMENA',to_date('10/08/07','DD/MM/RR'),'+1','515.127.4566','PU_CLERK',to_date('10/08/07','DD/MM/RR'),'2500','114','30');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('120','Matthew','Weiss','MWEISS',to_date('18/07/04','DD/MM/RR'),'+1','650.123.1234','ST_MAN',to_date('18/07/04','DD/MM/RR'),'8000','100','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('121','Adam','Fripp','AFRIPP',to_date('10/04/05','DD/MM/RR'),'+1','650.123.2234','ST_MAN',to_date('10/04/05','DD/MM/RR'),'8200','100','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('122','Payam','Kaufling','PKAUFLIN',to_date('01/05/03','DD/MM/RR'),'+1','650.123.3234','ST_MAN',to_date('01/01/08','DD/MM/RR'),'7900','100','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('123','Shanta','Vollman','SVOLLMAN',to_date('10/10/05','DD/MM/RR'),'+1','650.123.4234','ST_MAN',to_date('10/10/05','DD/MM/RR'),'6500','100','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('124','Kevin','Mourgos','KMOURGOS',to_date('16/11/07','DD/MM/RR'),'+1','650.123.5234','ST_MAN',to_date('16/11/07','DD/MM/RR'),'5800','100','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('125','Julia','Nayer','JNAYER',to_date('16/07/05','DD/MM/RR'),'+1','650.124.1214','ST_CLERK',to_date('16/07/05','DD/MM/RR'),'3200','120','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('126','Irene','Mikkilineni','IMIKKILI',to_date('28/09/06','DD/MM/RR'),'+1','650.124.1224','ST_CLERK',to_date('28/09/06','DD/MM/RR'),'2700','120','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('127','James','Landry','JLANDRY',to_date('14/01/07','DD/MM/RR'),'+1','650.124.1334','ST_CLERK',to_date('14/01/07','DD/MM/RR'),'2400','120','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('128','Steven','Markle','SMARKLE',to_date('08/03/08','DD/MM/RR'),'+1','650.124.1434','ST_CLERK',to_date('08/03/08','DD/MM/RR'),'2200','120','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('129','Laura','Bissot','LBISSOT',to_date('20/08/05','DD/MM/RR'),'+1','650.124.5234','ST_CLERK',to_date('20/08/05','DD/MM/RR'),'3300','121','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('130','Mozhe','Atkinson','MATKINSO',to_date('30/10/05','DD/MM/RR'),'+1','650.124.6234','ST_CLERK',to_date('30/10/05','DD/MM/RR'),'2800','121','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('131','James','Marlow','JAMRLOW',to_date('16/02/05','DD/MM/RR'),'+1','650.124.7234','ST_CLERK',to_date('16/02/05','DD/MM/RR'),'2500','121','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('132','TJ','Olson','TJOLSON',to_date('10/04/07','DD/MM/RR'),'+1','650.124.8234','ST_CLERK',to_date('10/04/07','DD/MM/RR'),'2100','121','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('133','Jason','Mallin','JMALLIN',to_date('14/06/04','DD/MM/RR'),'+1','650.127.1934','ST_CLERK',to_date('14/06/04','DD/MM/RR'),'3300','122','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('134','Michael','Rogers','MROGERS',to_date('26/08/06','DD/MM/RR'),'+1','650.127.1834','ST_CLERK',to_date('26/08/06','DD/MM/RR'),'2900','122','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('135','Ki','Gee','KGEE',to_date('12/12/07','DD/MM/RR'),'+1','650.127.1734','ST_CLERK',to_date('12/12/07','DD/MM/RR'),'2400','122','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('136','Hazel','Philtanker','HPHILTAN',to_date('06/02/08','DD/MM/RR'),'+1','650.127.1634','ST_CLERK',to_date('06/02/08','DD/MM/RR'),'2200','122','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('137','Renske','Ladwig','RLADWIG',to_date('14/07/03','DD/MM/RR'),'+1','650.121.1234','ST_CLERK',to_date('14/07/03','DD/MM/RR'),'3600','123','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('138','Stephen','Stiles','SSTILES',to_date('26/10/05','DD/MM/RR'),'+1','650.121.2034','ST_CLERK',to_date('26/10/05','DD/MM/RR'),'3200','123','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('139','John','Seo','JSEO',to_date('12/02/06','DD/MM/RR'),'+1','650.121.2019','ST_CLERK',to_date('12/02/06','DD/MM/RR'),'2700','123','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('140','Joshua','Patel','JPATEL',to_date('06/04/06','DD/MM/RR'),'+1','650.121.1834','ST_CLERK',to_date('06/04/06','DD/MM/RR'),'2500','123','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('141','Trenna','Rajs','TRAJS',to_date('17/10/03','DD/MM/RR'),'+1','650.121.8009','ST_CLERK',to_date('17/10/03','DD/MM/RR'),'3500','124','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('142','Curtis','Davies','CDAVIES',to_date('29/01/05','DD/MM/RR'),'+1','650.121.2994','ST_CLERK',to_date('29/01/05','DD/MM/RR'),'3100','124','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('143','Randall','Matos','RMATOS',to_date('15/03/06','DD/MM/RR'),'+1','650.121.2874','ST_CLERK',to_date('15/03/06','DD/MM/RR'),'2600','124','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('144','Peter','Vargas','PVARGAS',to_date('09/07/06','DD/MM/RR'),'+1','650.121.2004','ST_CLERK',to_date('09/07/06','DD/MM/RR'),'2500','124','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('145','John','Russell','JRUSSEL',to_date('01/10/04','DD/MM/RR'),'+44','1344.429268','SA_MAN',to_date('01/10/04','DD/MM/RR'),'14000','100','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('146','Karen','Partners','KPARTNER',to_date('05/01/05','DD/MM/RR'),'+44','1344.467268','SA_MAN',to_date('05/01/05','DD/MM/RR'),'13500','100','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('147','Alberto','Errazuriz','AERRAZUR',to_date('10/03/05','DD/MM/RR'),'+44','1344.429278','SA_MAN',to_date('10/03/05','DD/MM/RR'),'12000','100','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('148','Gerald','Cambrault','GCAMBRAU',to_date('15/10/07','DD/MM/RR'),'+44','1344.619268','SA_MAN',to_date('15/10/07','DD/MM/RR'),'11000','100','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('149','Eleni','Zlotkey','EZLOTKEY',to_date('29/01/08','DD/MM/RR'),'+44','1344.429018','SA_MAN',to_date('29/01/08','DD/MM/RR'),'10500','100','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('150','Peter','Tucker','PTUCKER',to_date('30/01/05','DD/MM/RR'),'+44','1344.129268','SA_REP',to_date('30/01/05','DD/MM/RR'),'10000','145','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('151','David','Bernstein','DBERNSTE',to_date('24/03/05','DD/MM/RR'),'+44','1344.345268','SA_REP',to_date('24/03/05','DD/MM/RR'),'9500','145','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('152','Peter','Hall','PHALL',to_date('20/08/05','DD/MM/RR'),'+44','1344.478968','SA_REP',to_date('20/08/05','DD/MM/RR'),'9000','145','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('153','Christopher','Olsen','COLSEN',to_date('30/03/06','DD/MM/RR'),'+44','1344.498718','SA_REP',to_date('30/03/06','DD/MM/RR'),'8000','145','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('154','Nanette','Cambrault','NCAMBRAU',to_date('09/12/06','DD/MM/RR'),'+44','1344.987668','SA_REP',to_date('09/12/06','DD/MM/RR'),'7500','145','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('155','Oliver','Tuvault','OTUVAULT',to_date('23/11/07','DD/MM/RR'),'+44','1344.486508','SA_REP',to_date('23/11/07','DD/MM/RR'),'7000','145','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('156','Janette','King','JKING',to_date('30/01/04','DD/MM/RR'),'+44','1345.429268','SA_REP',to_date('30/01/04','DD/MM/RR'),'10000','146','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('157','Patrick','Sully','PSULLY',to_date('04/03/04','DD/MM/RR'),'+44','1345.929268','SA_REP',to_date('04/03/04','DD/MM/RR'),'9500','146','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('158','Allan','McEwen','AMCEWEN',to_date('01/08/04','DD/MM/RR'),'+44','1345.829268','SA_REP',to_date('01/08/04','DD/MM/RR'),'9000','146','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('159','Lindsey','Smith','LSMITH',to_date('10/03/05','DD/MM/RR'),'+44','1345.729268','SA_REP',to_date('10/03/05','DD/MM/RR'),'8000','146','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('160','Louise','Doran','LDORAN',to_date('15/12/05','DD/MM/RR'),'+44','1345.629268','SA_REP',to_date('15/12/05','DD/MM/RR'),'7500','146','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('161','Sarath','Sewall','SSEWALL',to_date('03/11/06','DD/MM/RR'),'+44','1345.529268','SA_REP',to_date('03/11/06','DD/MM/RR'),'7000','146','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('162','Clara','Vishney','CVISHNEY',to_date('11/11/05','DD/MM/RR'),'+44','1346.129268','SA_REP',to_date('11/11/05','DD/MM/RR'),'10500','147','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('163','Danielle','Greene','DGREENE',to_date('19/03/07','DD/MM/RR'),'+44','1346.229268','SA_REP',to_date('19/03/07','DD/MM/RR'),'9500','147','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('164','Mattea','Marvins','MMARVINS',to_date('24/01/08','DD/MM/RR'),'+44','1346.329268','SA_REP',to_date('24/01/08','DD/MM/RR'),'7200','147','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('165','David','Lee','DLEE',to_date('23/02/08','DD/MM/RR'),'+44','1346.529268','SA_REP',to_date('23/02/08','DD/MM/RR'),'6800','147','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('166','Sundar','Ande','SANDE',to_date('24/03/08','DD/MM/RR'),'+44','1346.629268','SA_REP',to_date('24/03/08','DD/MM/RR'),'6400','147','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('167','Amit','Banda','ABANDA',to_date('21/04/08','DD/MM/RR'),'+44','1346.729268','SA_REP',to_date('21/04/08','DD/MM/RR'),'6200','147','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('168','Lisa','Ozer','LOZER',to_date('11/03/05','DD/MM/RR'),'+44','1343.929268','SA_REP',to_date('11/03/05','DD/MM/RR'),'11500','148','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('169','Harrison','Bloom','HBLOOM',to_date('23/03/06','DD/MM/RR'),'+44','1343.829268','SA_REP',to_date('23/03/06','DD/MM/RR'),'10000','148','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('170','Tayler','Fox','TFOX',to_date('24/01/06','DD/MM/RR'),'+44','1343.729268','SA_REP',to_date('24/01/06','DD/MM/RR'),'9600','148','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('171','William','Smith','WSMITH',to_date('23/02/07','DD/MM/RR'),'+44','1343.629268','SA_REP',to_date('23/02/07','DD/MM/RR'),'7400','148','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('172','Elizabeth','Bates','EBATES',to_date('24/03/07','DD/MM/RR'),'+44','1343.529268','SA_REP',to_date('24/03/07','DD/MM/RR'),'7300','148','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('173','Sundita','Kumar','SKUMAR',to_date('21/04/08','DD/MM/RR'),'+44','1343.329268','SA_REP',to_date('21/04/08','DD/MM/RR'),'6100','148','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('174','Ellen','Abel','EABEL',to_date('11/05/04','DD/MM/RR'),'+44','1644.429267','SA_REP',to_date('11/05/04','DD/MM/RR'),'11000','149','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('175','Alyssa','Hutton','AHUTTON',to_date('19/03/05','DD/MM/RR'),'+44','1644.429266','SA_REP',to_date('19/03/05','DD/MM/RR'),'8800','149','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('176','Jonathon','Taylor','JTAYLOR',to_date('24/03/06','DD/MM/RR'),'+44','1644.429265','SA_REP',to_date('01/01/08','DD/MM/RR'),'8600','149','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('177','Jack','Livingston','JLIVINGS',to_date('23/04/06','DD/MM/RR'),'+44','1644.429264','SA_REP',to_date('23/04/06','DD/MM/RR'),'8400','149','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('178','Kimberely','Grant','KGRANT',to_date('24/05/07','DD/MM/RR'),'+44','1644.429263','SA_REP',to_date('24/05/07','DD/MM/RR'),'7000','149',null);
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('179','Charles','Johnson','CJOHNSON',to_date('04/01/08','DD/MM/RR'),'+44','1644.429262','SA_REP',to_date('04/01/08','DD/MM/RR'),'6200','149','80');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('180','Winston','Taylor','WTAYLOR',to_date('24/01/06','DD/MM/RR'),'+1','650.507.9876','SH_CLERK',to_date('24/01/06','DD/MM/RR'),'3200','120','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('181','Jean','Fleaur','JFLEAUR',to_date('23/02/06','DD/MM/RR'),'+1','650.507.9877','SH_CLERK',to_date('23/02/06','DD/MM/RR'),'3100','120','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('182','Martha','Sullivan','MSULLIVA',to_date('21/06/07','DD/MM/RR'),'+1','650.507.9878','SH_CLERK',to_date('21/06/07','DD/MM/RR'),'2500','120','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('183','Girard','Geoni','GGEONI',to_date('03/02/08','DD/MM/RR'),'+1','650.507.9879','SH_CLERK',to_date('03/02/08','DD/MM/RR'),'2800','120','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('184','Nandita','Sarchand','NSARCHAN',to_date('27/01/04','DD/MM/RR'),'+1','650.509.1876','SH_CLERK',to_date('27/01/04','DD/MM/RR'),'4200','121','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('185','Alexis','Bull','ABULL',to_date('20/02/05','DD/MM/RR'),'+1','650.509.2876','SH_CLERK',to_date('20/02/05','DD/MM/RR'),'4100','121','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('186','Julia','Dellinger','JDELLING',to_date('24/06/06','DD/MM/RR'),'+1','650.509.3876','SH_CLERK',to_date('24/06/06','DD/MM/RR'),'3400','121','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('187','Anthony','Cabrio','ACABRIO',to_date('07/02/07','DD/MM/RR'),'+1','650.509.4876','SH_CLERK',to_date('07/02/07','DD/MM/RR'),'3000','121','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('188','Kelly','Chung','KCHUNG',to_date('14/06/05','DD/MM/RR'),'+1','650.505.1876','SH_CLERK',to_date('14/06/05','DD/MM/RR'),'3800','122','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('189','Jennifer','Dilly','JDILLY',to_date('13/08/05','DD/MM/RR'),'+1','650.505.2876','SH_CLERK',to_date('13/08/05','DD/MM/RR'),'3600','122','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('190','Timothy','Gates','TGATES',to_date('11/07/06','DD/MM/RR'),'+1','650.505.3876','SH_CLERK',to_date('11/07/06','DD/MM/RR'),'2900','122','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('191','Randall','Perkins','RPERKINS',to_date('19/12/07','DD/MM/RR'),'+1','650.505.4876','SH_CLERK',to_date('19/12/07','DD/MM/RR'),'2500','122','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('192','Sarah','Bell','SBELL',to_date('04/02/04','DD/MM/RR'),'+1','650.501.1876','SH_CLERK',to_date('04/02/04','DD/MM/RR'),'4000','123','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('193','Britney','Everett','BEVERETT',to_date('03/03/05','DD/MM/RR'),'+1','650.501.2876','SH_CLERK',to_date('03/03/05','DD/MM/RR'),'3900','123','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('194','Samuel','McCain','SMCCAIN',to_date('01/07/06','DD/MM/RR'),'+1','650.501.3876','SH_CLERK',to_date('01/07/06','DD/MM/RR'),'3200','123','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('195','Vance','Jones','VJONES',to_date('17/03/07','DD/MM/RR'),'+1','650.501.4876','SH_CLERK',to_date('17/03/07','DD/MM/RR'),'2800','123','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('196','Alana','Walsh','AWALSH',to_date('24/04/06','DD/MM/RR'),'+1','650.507.9811','SH_CLERK',to_date('24/04/06','DD/MM/RR'),'3100','124','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('197','Kevin','Feeney','KFEENEY',to_date('23/05/06','DD/MM/RR'),'+1','650.507.9822','SH_CLERK',to_date('23/05/06','DD/MM/RR'),'3000','124','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('198','Donald','OConnell','DOCONNEL',to_date('21/06/07','DD/MM/RR'),'+1','650.507.9833','SH_CLERK',to_date('21/06/07','DD/MM/RR'),'2600','124','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('199','Douglas','Grant','DGRANT',to_date('13/01/08','DD/MM/RR'),'+1','650.507.9844','SH_CLERK',to_date('13/01/08','DD/MM/RR'),'2600','124','50');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('200','Jennifer','Whalen','JWHALEN',to_date('17/09/03','DD/MM/RR'),'+1','515.123.4444','AD_ASST',to_date('01/01/07','DD/MM/RR'),'4400','101','10');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('201','Michael','Hartstein','MHARTSTE',to_date('17/02/04','DD/MM/RR'),'+1','515.123.5555','MK_MAN',to_date('20/12/07','DD/MM/RR'),'13000','100','20');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('202','Pat','Fay','PFAY',to_date('17/08/05','DD/MM/RR'),'+1','603.123.6666','MK_REP',to_date('17/08/05','DD/MM/RR'),'6000','201','20');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('203','Susan','Mavris','SMAVRIS',to_date('07/06/02','DD/MM/RR'),'+1','515.123.7777','HR_REP',to_date('07/06/02','DD/MM/RR'),'6500','101','40');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('204','Hermann','Baer','HBAER',to_date('07/06/02','DD/MM/RR'),'+1','515.123.8888','PR_REP',to_date('07/06/02','DD/MM/RR'),'10000','101','70');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('205','Shelley','Higgins','SHIGGINS',to_date('07/06/02','DD/MM/RR'),'+1','515.123.8080','AC_MGR',to_date('07/06/02','DD/MM/RR'),'12008','101','110');
Insert into APP_DATA.EMPLOYEES# (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('206','William','Gietz','WGIETZ',to_date('07/06/02','DD/MM/RR'),'+1','515.123.8181','AC_ACCOUNT',to_date('07/06/02','DD/MM/RR'),'8300','205','110');
REM INSERTING into APP_DATA.JOB_HISTORY#
SET DEFINE OFF;
Insert into APP_DATA.JOB_HISTORY# (EMPLOYEE_ID,JOB_ID,START_DATE,END_DATE,DEPARTMENT_ID) values ('102','IT_PROG',to_date('13/01/01','DD/MM/RR'),to_date('24/07/06','DD/MM/RR'),'60');
Insert into APP_DATA.JOB_HISTORY# (EMPLOYEE_ID,JOB_ID,START_DATE,END_DATE,DEPARTMENT_ID) values ('101','AC_ACCOUNT',to_date('21/09/97','DD/MM/RR'),to_date('27/10/01','DD/MM/RR'),'110');
Insert into APP_DATA.JOB_HISTORY# (EMPLOYEE_ID,JOB_ID,START_DATE,END_DATE,DEPARTMENT_ID) values ('101','AC_MGR',to_date('28/10/01','DD/MM/RR'),to_date('15/03/05','DD/MM/RR'),'110');
Insert into APP_DATA.JOB_HISTORY# (EMPLOYEE_ID,JOB_ID,START_DATE,END_DATE,DEPARTMENT_ID) values ('201','MK_REP',to_date('17/02/04','DD/MM/RR'),to_date('19/12/07','DD/MM/RR'),'20');
Insert into APP_DATA.JOB_HISTORY# (EMPLOYEE_ID,JOB_ID,START_DATE,END_DATE,DEPARTMENT_ID) values ('114','ST_CLERK',to_date('24/03/06','DD/MM/RR'),to_date('31/12/07','DD/MM/RR'),'50');
Insert into APP_DATA.JOB_HISTORY# (EMPLOYEE_ID,JOB_ID,START_DATE,END_DATE,DEPARTMENT_ID) values ('122','ST_CLERK',to_date('01/01/07','DD/MM/RR'),to_date('31/12/07','DD/MM/RR'),'50');
Insert into APP_DATA.JOB_HISTORY# (EMPLOYEE_ID,JOB_ID,START_DATE,END_DATE,DEPARTMENT_ID) values ('200','AD_ASST',to_date('17/09/95','DD/MM/RR'),to_date('17/06/01','DD/MM/RR'),'90');
Insert into APP_DATA.JOB_HISTORY# (EMPLOYEE_ID,JOB_ID,START_DATE,END_DATE,DEPARTMENT_ID) values ('176','SA_REP',to_date('24/03/06','DD/MM/RR'),to_date('31/12/06','DD/MM/RR'),'80');
Insert into APP_DATA.JOB_HISTORY# (EMPLOYEE_ID,JOB_ID,START_DATE,END_DATE,DEPARTMENT_ID) values ('176','SA_MAN',to_date('01/01/07','DD/MM/RR'),to_date('31/12/07','DD/MM/RR'),'80');
Insert into APP_DATA.JOB_HISTORY# (EMPLOYEE_ID,JOB_ID,START_DATE,END_DATE,DEPARTMENT_ID) values ('200','AC_ACCOUNT',to_date('01/07/02','DD/MM/RR'),to_date('31/12/06','DD/MM/RR'),'90');
REM INSERTING into APP_DATA.JOBS#
SET DEFINE OFF;
Insert into APP_DATA.JOBS# (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('AD_PRES','President','20080','40000');
Insert into APP_DATA.JOBS# (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('AD_VP','Administration Vice President','15000','30000');
Insert into APP_DATA.JOBS# (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('AD_ASST','Administration Assistant','3000','6000');
Insert into APP_DATA.JOBS# (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('FI_MGR','Finance Manager','8200','16000');
Insert into APP_DATA.JOBS# (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('FI_ACCOUNT','Accountant','4200','9000');
Insert into APP_DATA.JOBS# (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('AC_MGR','Accounting Manager','8200','16000');
Insert into APP_DATA.JOBS# (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('AC_ACCOUNT','Public Accountant','4200','9000');
Insert into APP_DATA.JOBS# (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('SA_MAN','Sales Manager','10000','20080');
Insert into APP_DATA.JOBS# (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('SA_REP','Sales Representative','6000','12008');
Insert into APP_DATA.JOBS# (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('PU_MAN','Purchasing Manager','8000','15000');
Insert into APP_DATA.JOBS# (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('PU_CLERK','Purchasing Clerk','2500','5500');
Insert into APP_DATA.JOBS# (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('ST_MAN','Stock Manager','5500','8500');
Insert into APP_DATA.JOBS# (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('ST_CLERK','Stock Clerk','2008','5000');
Insert into APP_DATA.JOBS# (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('SH_CLERK','Shipping Clerk','2500','5500');
Insert into APP_DATA.JOBS# (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('IT_PROG','Programmer','4000','10000');
Insert into APP_DATA.JOBS# (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('MK_MAN','Marketing Manager','9000','15000');
Insert into APP_DATA.JOBS# (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('MK_REP','Marketing Representative','4000','9000');
Insert into APP_DATA.JOBS# (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('HR_REP','Human Resources Representative','4000','9000');
Insert into APP_DATA.JOBS# (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('PR_REP','Public Relations Representative','4500','10500');
REM INSERTING into APP_DATA.DEPARTMENTS
SET DEFINE OFF;
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('10','Administration','200');
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('20','Marketing','201');
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('30','Purchasing','114');
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('40','Human Resources','203');
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('50','Shipping','121');
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('60','IT','103');
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('70','Public Relations','204');
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('80','Sales','145');
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('90','Executive','100');
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('100','Finance','108');
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('110','Accounting','205');
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('120','Treasury',null);
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('130','Corporate Tax',null);
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('140','Control And Credit',null);
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('150','Shareholder Services',null);
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('160','Benefits',null);
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('170','Manufacturing',null);
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('180','Construction',null);
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('190','Contracting',null);
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('200','Operations',null);
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('210','IT Support',null);
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('220','NOC',null);
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('230','IT Helpdesk',null);
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('240','Government Sales',null);
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('250','Retail Sales',null);
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('260','Recruiting',null);
Insert into APP_DATA.DEPARTMENTS (DEPARTMENT_ID,DEPARTMENT_NAME,MANAGER_ID) values ('270','Payroll',null);
REM INSERTING into APP_DATA.EMPLOYEES
SET DEFINE OFF;
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('100','Steven','King','SKING',to_date('17/06/03','DD/MM/RR'),'+1','515.123.4567','AD_PRES',to_date('17/06/03','DD/MM/RR'),'24000',null,'90');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('101','Neena','Kochhar','NKOCHHAR',to_date('21/09/05','DD/MM/RR'),'+1','515.123.4568','AD_VP',to_date('16/03/05','DD/MM/RR'),'17000','100','90');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('102','Lex','De Haan','LDEHAAN',to_date('13/01/01','DD/MM/RR'),'+1','515.123.4569','AD_VP',to_date('25/07/06','DD/MM/RR'),'17000','100','90');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('103','Alexander','Hunold','AHUNOLD',to_date('03/01/06','DD/MM/RR'),'+1','590.423.4567','IT_PROG',to_date('03/01/06','DD/MM/RR'),'9000','102','60');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('104','Bruce','Ernst','BERNST',to_date('21/05/07','DD/MM/RR'),'+1','590.423.4568','IT_PROG',to_date('21/05/07','DD/MM/RR'),'6000','103','60');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('105','David','Austin','DAUSTIN',to_date('25/06/05','DD/MM/RR'),'+1','590.423.4569','IT_PROG',to_date('25/06/05','DD/MM/RR'),'4800','103','60');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('106','Valli','Pataballa','VPATABAL',to_date('05/02/06','DD/MM/RR'),'+1','590.423.4560','IT_PROG',to_date('05/02/06','DD/MM/RR'),'4800','103','60');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('107','Diana','Lorentz','DLORENTZ',to_date('07/02/07','DD/MM/RR'),'+1','590.423.5567','IT_PROG',to_date('07/02/07','DD/MM/RR'),'4200','103','60');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('108','Nancy','Greenberg','NGREENBE',to_date('17/08/02','DD/MM/RR'),'+1','515.124.4569','FI_MGR',to_date('17/08/02','DD/MM/RR'),'12008','101','100');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('109','Daniel','Faviet','DFAVIET',to_date('16/08/02','DD/MM/RR'),'+1','515.124.4169','FI_ACCOUNT',to_date('16/08/02','DD/MM/RR'),'9000','108','100');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('110','John','Chen','JCHEN',to_date('28/09/05','DD/MM/RR'),'+1','515.124.4269','FI_ACCOUNT',to_date('28/09/05','DD/MM/RR'),'8200','108','100');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('111','Ismael','Sciarra','ISCIARRA',to_date('30/09/05','DD/MM/RR'),'+1','515.124.4369','FI_ACCOUNT',to_date('30/09/05','DD/MM/RR'),'7700','108','100');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('112','Jose Manuel','Urman','JMURMAN',to_date('07/03/06','DD/MM/RR'),'+1','515.124.4469','FI_ACCOUNT',to_date('07/03/06','DD/MM/RR'),'7800','108','100');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('113','Luis','Popp','LPOPP',to_date('07/12/07','DD/MM/RR'),'+1','515.124.4567','FI_ACCOUNT',to_date('07/12/07','DD/MM/RR'),'6900','108','100');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('114','Den','Raphaely','DRAPHEAL',to_date('07/12/02','DD/MM/RR'),'+1','515.127.4561','PU_MAN',to_date('01/01/08','DD/MM/RR'),'11000','100','30');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('115','Alexander','Khoo','AKHOO',to_date('18/05/03','DD/MM/RR'),'+1','515.127.4562','PU_CLERK',to_date('18/05/03','DD/MM/RR'),'3100','114','30');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('116','Shelli','Baida','SBAIDA',to_date('24/12/05','DD/MM/RR'),'+1','515.127.4563','PU_CLERK',to_date('24/12/05','DD/MM/RR'),'2900','114','30');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('117','Sigal','Tobias','STOBIAS',to_date('24/07/05','DD/MM/RR'),'+1','515.127.4564','PU_CLERK',to_date('24/07/05','DD/MM/RR'),'2800','114','30');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('118','Guy','Himuro','GHIMURO',to_date('15/11/06','DD/MM/RR'),'+1','515.127.4565','PU_CLERK',to_date('15/11/06','DD/MM/RR'),'2600','114','30');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('119','Karen','Colmenares','KCOLMENA',to_date('10/08/07','DD/MM/RR'),'+1','515.127.4566','PU_CLERK',to_date('10/08/07','DD/MM/RR'),'2500','114','30');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('120','Matthew','Weiss','MWEISS',to_date('18/07/04','DD/MM/RR'),'+1','650.123.1234','ST_MAN',to_date('18/07/04','DD/MM/RR'),'8000','100','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('121','Adam','Fripp','AFRIPP',to_date('10/04/05','DD/MM/RR'),'+1','650.123.2234','ST_MAN',to_date('10/04/05','DD/MM/RR'),'8200','100','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('122','Payam','Kaufling','PKAUFLIN',to_date('01/05/03','DD/MM/RR'),'+1','650.123.3234','ST_MAN',to_date('01/01/08','DD/MM/RR'),'7900','100','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('123','Shanta','Vollman','SVOLLMAN',to_date('10/10/05','DD/MM/RR'),'+1','650.123.4234','ST_MAN',to_date('10/10/05','DD/MM/RR'),'6500','100','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('124','Kevin','Mourgos','KMOURGOS',to_date('16/11/07','DD/MM/RR'),'+1','650.123.5234','ST_MAN',to_date('16/11/07','DD/MM/RR'),'5800','100','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('125','Julia','Nayer','JNAYER',to_date('16/07/05','DD/MM/RR'),'+1','650.124.1214','ST_CLERK',to_date('16/07/05','DD/MM/RR'),'3200','120','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('126','Irene','Mikkilineni','IMIKKILI',to_date('28/09/06','DD/MM/RR'),'+1','650.124.1224','ST_CLERK',to_date('28/09/06','DD/MM/RR'),'2700','120','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('127','James','Landry','JLANDRY',to_date('14/01/07','DD/MM/RR'),'+1','650.124.1334','ST_CLERK',to_date('14/01/07','DD/MM/RR'),'2400','120','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('128','Steven','Markle','SMARKLE',to_date('08/03/08','DD/MM/RR'),'+1','650.124.1434','ST_CLERK',to_date('08/03/08','DD/MM/RR'),'2200','120','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('129','Laura','Bissot','LBISSOT',to_date('20/08/05','DD/MM/RR'),'+1','650.124.5234','ST_CLERK',to_date('20/08/05','DD/MM/RR'),'3300','121','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('130','Mozhe','Atkinson','MATKINSO',to_date('30/10/05','DD/MM/RR'),'+1','650.124.6234','ST_CLERK',to_date('30/10/05','DD/MM/RR'),'2800','121','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('131','James','Marlow','JAMRLOW',to_date('16/02/05','DD/MM/RR'),'+1','650.124.7234','ST_CLERK',to_date('16/02/05','DD/MM/RR'),'2500','121','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('132','TJ','Olson','TJOLSON',to_date('10/04/07','DD/MM/RR'),'+1','650.124.8234','ST_CLERK',to_date('10/04/07','DD/MM/RR'),'2100','121','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('133','Jason','Mallin','JMALLIN',to_date('14/06/04','DD/MM/RR'),'+1','650.127.1934','ST_CLERK',to_date('14/06/04','DD/MM/RR'),'3300','122','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('134','Michael','Rogers','MROGERS',to_date('26/08/06','DD/MM/RR'),'+1','650.127.1834','ST_CLERK',to_date('26/08/06','DD/MM/RR'),'2900','122','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('135','Ki','Gee','KGEE',to_date('12/12/07','DD/MM/RR'),'+1','650.127.1734','ST_CLERK',to_date('12/12/07','DD/MM/RR'),'2400','122','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('136','Hazel','Philtanker','HPHILTAN',to_date('06/02/08','DD/MM/RR'),'+1','650.127.1634','ST_CLERK',to_date('06/02/08','DD/MM/RR'),'2200','122','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('137','Renske','Ladwig','RLADWIG',to_date('14/07/03','DD/MM/RR'),'+1','650.121.1234','ST_CLERK',to_date('14/07/03','DD/MM/RR'),'3600','123','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('138','Stephen','Stiles','SSTILES',to_date('26/10/05','DD/MM/RR'),'+1','650.121.2034','ST_CLERK',to_date('26/10/05','DD/MM/RR'),'3200','123','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('139','John','Seo','JSEO',to_date('12/02/06','DD/MM/RR'),'+1','650.121.2019','ST_CLERK',to_date('12/02/06','DD/MM/RR'),'2700','123','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('140','Joshua','Patel','JPATEL',to_date('06/04/06','DD/MM/RR'),'+1','650.121.1834','ST_CLERK',to_date('06/04/06','DD/MM/RR'),'2500','123','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('141','Trenna','Rajs','TRAJS',to_date('17/10/03','DD/MM/RR'),'+1','650.121.8009','ST_CLERK',to_date('17/10/03','DD/MM/RR'),'3500','124','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('142','Curtis','Davies','CDAVIES',to_date('29/01/05','DD/MM/RR'),'+1','650.121.2994','ST_CLERK',to_date('29/01/05','DD/MM/RR'),'3100','124','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('143','Randall','Matos','RMATOS',to_date('15/03/06','DD/MM/RR'),'+1','650.121.2874','ST_CLERK',to_date('15/03/06','DD/MM/RR'),'2600','124','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('144','Peter','Vargas','PVARGAS',to_date('09/07/06','DD/MM/RR'),'+1','650.121.2004','ST_CLERK',to_date('09/07/06','DD/MM/RR'),'2500','124','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('145','John','Russell','JRUSSEL',to_date('01/10/04','DD/MM/RR'),'+44','1344.429268','SA_MAN',to_date('01/10/04','DD/MM/RR'),'14000','100','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('146','Karen','Partners','KPARTNER',to_date('05/01/05','DD/MM/RR'),'+44','1344.467268','SA_MAN',to_date('05/01/05','DD/MM/RR'),'13500','100','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('147','Alberto','Errazuriz','AERRAZUR',to_date('10/03/05','DD/MM/RR'),'+44','1344.429278','SA_MAN',to_date('10/03/05','DD/MM/RR'),'12000','100','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('148','Gerald','Cambrault','GCAMBRAU',to_date('15/10/07','DD/MM/RR'),'+44','1344.619268','SA_MAN',to_date('15/10/07','DD/MM/RR'),'11000','100','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('149','Eleni','Zlotkey','EZLOTKEY',to_date('29/01/08','DD/MM/RR'),'+44','1344.429018','SA_MAN',to_date('29/01/08','DD/MM/RR'),'10500','100','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('150','Peter','Tucker','PTUCKER',to_date('30/01/05','DD/MM/RR'),'+44','1344.129268','SA_REP',to_date('30/01/05','DD/MM/RR'),'10000','145','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('151','David','Bernstein','DBERNSTE',to_date('24/03/05','DD/MM/RR'),'+44','1344.345268','SA_REP',to_date('24/03/05','DD/MM/RR'),'9500','145','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('152','Peter','Hall','PHALL',to_date('20/08/05','DD/MM/RR'),'+44','1344.478968','SA_REP',to_date('20/08/05','DD/MM/RR'),'9000','145','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('153','Christopher','Olsen','COLSEN',to_date('30/03/06','DD/MM/RR'),'+44','1344.498718','SA_REP',to_date('30/03/06','DD/MM/RR'),'8000','145','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('154','Nanette','Cambrault','NCAMBRAU',to_date('09/12/06','DD/MM/RR'),'+44','1344.987668','SA_REP',to_date('09/12/06','DD/MM/RR'),'7500','145','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('155','Oliver','Tuvault','OTUVAULT',to_date('23/11/07','DD/MM/RR'),'+44','1344.486508','SA_REP',to_date('23/11/07','DD/MM/RR'),'7000','145','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('156','Janette','King','JKING',to_date('30/01/04','DD/MM/RR'),'+44','1345.429268','SA_REP',to_date('30/01/04','DD/MM/RR'),'10000','146','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('157','Patrick','Sully','PSULLY',to_date('04/03/04','DD/MM/RR'),'+44','1345.929268','SA_REP',to_date('04/03/04','DD/MM/RR'),'9500','146','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('158','Allan','McEwen','AMCEWEN',to_date('01/08/04','DD/MM/RR'),'+44','1345.829268','SA_REP',to_date('01/08/04','DD/MM/RR'),'9000','146','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('159','Lindsey','Smith','LSMITH',to_date('10/03/05','DD/MM/RR'),'+44','1345.729268','SA_REP',to_date('10/03/05','DD/MM/RR'),'8000','146','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('160','Louise','Doran','LDORAN',to_date('15/12/05','DD/MM/RR'),'+44','1345.629268','SA_REP',to_date('15/12/05','DD/MM/RR'),'7500','146','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('161','Sarath','Sewall','SSEWALL',to_date('03/11/06','DD/MM/RR'),'+44','1345.529268','SA_REP',to_date('03/11/06','DD/MM/RR'),'7000','146','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('162','Clara','Vishney','CVISHNEY',to_date('11/11/05','DD/MM/RR'),'+44','1346.129268','SA_REP',to_date('11/11/05','DD/MM/RR'),'10500','147','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('163','Danielle','Greene','DGREENE',to_date('19/03/07','DD/MM/RR'),'+44','1346.229268','SA_REP',to_date('19/03/07','DD/MM/RR'),'9500','147','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('164','Mattea','Marvins','MMARVINS',to_date('24/01/08','DD/MM/RR'),'+44','1346.329268','SA_REP',to_date('24/01/08','DD/MM/RR'),'7200','147','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('165','David','Lee','DLEE',to_date('23/02/08','DD/MM/RR'),'+44','1346.529268','SA_REP',to_date('23/02/08','DD/MM/RR'),'6800','147','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('166','Sundar','Ande','SANDE',to_date('24/03/08','DD/MM/RR'),'+44','1346.629268','SA_REP',to_date('24/03/08','DD/MM/RR'),'6400','147','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('167','Amit','Banda','ABANDA',to_date('21/04/08','DD/MM/RR'),'+44','1346.729268','SA_REP',to_date('21/04/08','DD/MM/RR'),'6200','147','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('168','Lisa','Ozer','LOZER',to_date('11/03/05','DD/MM/RR'),'+44','1343.929268','SA_REP',to_date('11/03/05','DD/MM/RR'),'11500','148','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('169','Harrison','Bloom','HBLOOM',to_date('23/03/06','DD/MM/RR'),'+44','1343.829268','SA_REP',to_date('23/03/06','DD/MM/RR'),'10000','148','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('170','Tayler','Fox','TFOX',to_date('24/01/06','DD/MM/RR'),'+44','1343.729268','SA_REP',to_date('24/01/06','DD/MM/RR'),'9600','148','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('171','William','Smith','WSMITH',to_date('23/02/07','DD/MM/RR'),'+44','1343.629268','SA_REP',to_date('23/02/07','DD/MM/RR'),'7400','148','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('172','Elizabeth','Bates','EBATES',to_date('24/03/07','DD/MM/RR'),'+44','1343.529268','SA_REP',to_date('24/03/07','DD/MM/RR'),'7300','148','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('173','Sundita','Kumar','SKUMAR',to_date('21/04/08','DD/MM/RR'),'+44','1343.329268','SA_REP',to_date('21/04/08','DD/MM/RR'),'6100','148','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('174','Ellen','Abel','EABEL',to_date('11/05/04','DD/MM/RR'),'+44','1644.429267','SA_REP',to_date('11/05/04','DD/MM/RR'),'11000','149','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('175','Alyssa','Hutton','AHUTTON',to_date('19/03/05','DD/MM/RR'),'+44','1644.429266','SA_REP',to_date('19/03/05','DD/MM/RR'),'8800','149','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('176','Jonathon','Taylor','JTAYLOR',to_date('24/03/06','DD/MM/RR'),'+44','1644.429265','SA_REP',to_date('01/01/08','DD/MM/RR'),'8600','149','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('177','Jack','Livingston','JLIVINGS',to_date('23/04/06','DD/MM/RR'),'+44','1644.429264','SA_REP',to_date('23/04/06','DD/MM/RR'),'8400','149','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('178','Kimberely','Grant','KGRANT',to_date('24/05/07','DD/MM/RR'),'+44','1644.429263','SA_REP',to_date('24/05/07','DD/MM/RR'),'7000','149',null);
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('179','Charles','Johnson','CJOHNSON',to_date('04/01/08','DD/MM/RR'),'+44','1644.429262','SA_REP',to_date('04/01/08','DD/MM/RR'),'6200','149','80');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('180','Winston','Taylor','WTAYLOR',to_date('24/01/06','DD/MM/RR'),'+1','650.507.9876','SH_CLERK',to_date('24/01/06','DD/MM/RR'),'3200','120','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('181','Jean','Fleaur','JFLEAUR',to_date('23/02/06','DD/MM/RR'),'+1','650.507.9877','SH_CLERK',to_date('23/02/06','DD/MM/RR'),'3100','120','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('182','Martha','Sullivan','MSULLIVA',to_date('21/06/07','DD/MM/RR'),'+1','650.507.9878','SH_CLERK',to_date('21/06/07','DD/MM/RR'),'2500','120','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('183','Girard','Geoni','GGEONI',to_date('03/02/08','DD/MM/RR'),'+1','650.507.9879','SH_CLERK',to_date('03/02/08','DD/MM/RR'),'2800','120','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('184','Nandita','Sarchand','NSARCHAN',to_date('27/01/04','DD/MM/RR'),'+1','650.509.1876','SH_CLERK',to_date('27/01/04','DD/MM/RR'),'4200','121','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('185','Alexis','Bull','ABULL',to_date('20/02/05','DD/MM/RR'),'+1','650.509.2876','SH_CLERK',to_date('20/02/05','DD/MM/RR'),'4100','121','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('186','Julia','Dellinger','JDELLING',to_date('24/06/06','DD/MM/RR'),'+1','650.509.3876','SH_CLERK',to_date('24/06/06','DD/MM/RR'),'3400','121','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('187','Anthony','Cabrio','ACABRIO',to_date('07/02/07','DD/MM/RR'),'+1','650.509.4876','SH_CLERK',to_date('07/02/07','DD/MM/RR'),'3000','121','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('188','Kelly','Chung','KCHUNG',to_date('14/06/05','DD/MM/RR'),'+1','650.505.1876','SH_CLERK',to_date('14/06/05','DD/MM/RR'),'3800','122','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('189','Jennifer','Dilly','JDILLY',to_date('13/08/05','DD/MM/RR'),'+1','650.505.2876','SH_CLERK',to_date('13/08/05','DD/MM/RR'),'3600','122','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('190','Timothy','Gates','TGATES',to_date('11/07/06','DD/MM/RR'),'+1','650.505.3876','SH_CLERK',to_date('11/07/06','DD/MM/RR'),'2900','122','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('191','Randall','Perkins','RPERKINS',to_date('19/12/07','DD/MM/RR'),'+1','650.505.4876','SH_CLERK',to_date('19/12/07','DD/MM/RR'),'2500','122','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('192','Sarah','Bell','SBELL',to_date('04/02/04','DD/MM/RR'),'+1','650.501.1876','SH_CLERK',to_date('04/02/04','DD/MM/RR'),'4000','123','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('193','Britney','Everett','BEVERETT',to_date('03/03/05','DD/MM/RR'),'+1','650.501.2876','SH_CLERK',to_date('03/03/05','DD/MM/RR'),'3900','123','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('194','Samuel','McCain','SMCCAIN',to_date('01/07/06','DD/MM/RR'),'+1','650.501.3876','SH_CLERK',to_date('01/07/06','DD/MM/RR'),'3200','123','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('195','Vance','Jones','VJONES',to_date('17/03/07','DD/MM/RR'),'+1','650.501.4876','SH_CLERK',to_date('17/03/07','DD/MM/RR'),'2800','123','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('196','Alana','Walsh','AWALSH',to_date('24/04/06','DD/MM/RR'),'+1','650.507.9811','SH_CLERK',to_date('24/04/06','DD/MM/RR'),'3100','124','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('197','Kevin','Feeney','KFEENEY',to_date('23/05/06','DD/MM/RR'),'+1','650.507.9822','SH_CLERK',to_date('23/05/06','DD/MM/RR'),'3000','124','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('198','Donald','OConnell','DOCONNEL',to_date('21/06/07','DD/MM/RR'),'+1','650.507.9833','SH_CLERK',to_date('21/06/07','DD/MM/RR'),'2600','124','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('199','Douglas','Grant','DGRANT',to_date('13/01/08','DD/MM/RR'),'+1','650.507.9844','SH_CLERK',to_date('13/01/08','DD/MM/RR'),'2600','124','50');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('200','Jennifer','Whalen','JWHALEN',to_date('17/09/03','DD/MM/RR'),'+1','515.123.4444','AD_ASST',to_date('01/01/07','DD/MM/RR'),'4400','101','10');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('201','Michael','Hartstein','MHARTSTE',to_date('17/02/04','DD/MM/RR'),'+1','515.123.5555','MK_MAN',to_date('20/12/07','DD/MM/RR'),'13000','100','20');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('202','Pat','Fay','PFAY',to_date('17/08/05','DD/MM/RR'),'+1','603.123.6666','MK_REP',to_date('17/08/05','DD/MM/RR'),'6000','201','20');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('203','Susan','Mavris','SMAVRIS',to_date('07/06/02','DD/MM/RR'),'+1','515.123.7777','HR_REP',to_date('07/06/02','DD/MM/RR'),'6500','101','40');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('204','Hermann','Baer','HBAER',to_date('07/06/02','DD/MM/RR'),'+1','515.123.8888','PR_REP',to_date('07/06/02','DD/MM/RR'),'10000','101','70');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('205','Shelley','Higgins','SHIGGINS',to_date('07/06/02','DD/MM/RR'),'+1','515.123.8080','AC_MGR',to_date('07/06/02','DD/MM/RR'),'12008','101','110');
Insert into APP_DATA.EMPLOYEES (EMPLOYEE_ID,FIRST_NAME,LAST_NAME,EMAIL_ADDR,HIRE_DATE,COUNTRY_CODE,PHONE_NUMBER,JOB_ID,JOB_START_DATE,SALARY,MANAGER_ID,DEPARTMENT_ID) values ('206','William','Gietz','WGIETZ',to_date('07/06/02','DD/MM/RR'),'+1','515.123.8181','AC_ACCOUNT',to_date('07/06/02','DD/MM/RR'),'8300','205','110');
REM INSERTING into APP_DATA.JOB_HISTORY
SET DEFINE OFF;
Insert into APP_DATA.JOB_HISTORY (EMPLOYEE_ID,JOB_ID,START_DATE,END_DATE,DEPARTMENT_ID) values ('102','IT_PROG',to_date('13/01/01','DD/MM/RR'),to_date('24/07/06','DD/MM/RR'),'60');
Insert into APP_DATA.JOB_HISTORY (EMPLOYEE_ID,JOB_ID,START_DATE,END_DATE,DEPARTMENT_ID) values ('101','AC_ACCOUNT',to_date('21/09/97','DD/MM/RR'),to_date('27/10/01','DD/MM/RR'),'110');
Insert into APP_DATA.JOB_HISTORY (EMPLOYEE_ID,JOB_ID,START_DATE,END_DATE,DEPARTMENT_ID) values ('101','AC_MGR',to_date('28/10/01','DD/MM/RR'),to_date('15/03/05','DD/MM/RR'),'110');
Insert into APP_DATA.JOB_HISTORY (EMPLOYEE_ID,JOB_ID,START_DATE,END_DATE,DEPARTMENT_ID) values ('201','MK_REP',to_date('17/02/04','DD/MM/RR'),to_date('19/12/07','DD/MM/RR'),'20');
Insert into APP_DATA.JOB_HISTORY (EMPLOYEE_ID,JOB_ID,START_DATE,END_DATE,DEPARTMENT_ID) values ('114','ST_CLERK',to_date('24/03/06','DD/MM/RR'),to_date('31/12/07','DD/MM/RR'),'50');
Insert into APP_DATA.JOB_HISTORY (EMPLOYEE_ID,JOB_ID,START_DATE,END_DATE,DEPARTMENT_ID) values ('122','ST_CLERK',to_date('01/01/07','DD/MM/RR'),to_date('31/12/07','DD/MM/RR'),'50');
Insert into APP_DATA.JOB_HISTORY (EMPLOYEE_ID,JOB_ID,START_DATE,END_DATE,DEPARTMENT_ID) values ('200','AD_ASST',to_date('17/09/95','DD/MM/RR'),to_date('17/06/01','DD/MM/RR'),'90');
Insert into APP_DATA.JOB_HISTORY (EMPLOYEE_ID,JOB_ID,START_DATE,END_DATE,DEPARTMENT_ID) values ('176','SA_REP',to_date('24/03/06','DD/MM/RR'),to_date('31/12/06','DD/MM/RR'),'80');
Insert into APP_DATA.JOB_HISTORY (EMPLOYEE_ID,JOB_ID,START_DATE,END_DATE,DEPARTMENT_ID) values ('176','SA_MAN',to_date('01/01/07','DD/MM/RR'),to_date('31/12/07','DD/MM/RR'),'80');
Insert into APP_DATA.JOB_HISTORY (EMPLOYEE_ID,JOB_ID,START_DATE,END_DATE,DEPARTMENT_ID) values ('200','AC_ACCOUNT',to_date('01/07/02','DD/MM/RR'),to_date('31/12/06','DD/MM/RR'),'90');
REM INSERTING into APP_DATA.JOBS
SET DEFINE OFF;
Insert into APP_DATA.JOBS (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('AD_PRES','President','20080','40000');
Insert into APP_DATA.JOBS (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('AD_VP','Administration Vice President','15000','30000');
Insert into APP_DATA.JOBS (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('AD_ASST','Administration Assistant','3000','6000');
Insert into APP_DATA.JOBS (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('FI_MGR','Finance Manager','8200','16000');
Insert into APP_DATA.JOBS (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('FI_ACCOUNT','Accountant','4200','9000');
Insert into APP_DATA.JOBS (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('AC_MGR','Accounting Manager','8200','16000');
Insert into APP_DATA.JOBS (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('AC_ACCOUNT','Public Accountant','4200','9000');
Insert into APP_DATA.JOBS (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('SA_MAN','Sales Manager','10000','20080');
Insert into APP_DATA.JOBS (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('SA_REP','Sales Representative','6000','12008');
Insert into APP_DATA.JOBS (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('PU_MAN','Purchasing Manager','8000','15000');
Insert into APP_DATA.JOBS (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('PU_CLERK','Purchasing Clerk','2500','5500');
Insert into APP_DATA.JOBS (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('ST_MAN','Stock Manager','5500','8500');
Insert into APP_DATA.JOBS (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('ST_CLERK','Stock Clerk','2008','5000');
Insert into APP_DATA.JOBS (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('SH_CLERK','Shipping Clerk','2500','5500');
Insert into APP_DATA.JOBS (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('IT_PROG','Programmer','4000','10000');
Insert into APP_DATA.JOBS (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('MK_MAN','Marketing Manager','9000','15000');
Insert into APP_DATA.JOBS (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('MK_REP','Marketing Representative','4000','9000');
Insert into APP_DATA.JOBS (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('HR_REP','Human Resources Representative','4000','9000');
Insert into APP_DATA.JOBS (JOB_ID,JOB_TITLE,MIN_SALARY,MAX_SALARY) values ('PR_REP','Public Relations Representative','4500','10500');
--------------------------------------------------------
--  DDL for Index DEPARTMENTS#_DM_NOT_NULL
--------------------------------------------------------

  CREATE UNIQUE INDEX "APP_DATA"."DEPARTMENTS#_DM_NOT_NULL" ON "APP_DATA"."DEPARTMENTS#" ("DEPARTMENT_NAME") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index DEPARTMENTS#_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "APP_DATA"."DEPARTMENTS#_PK" ON "APP_DATA"."DEPARTMENTS#" ("DEPARTMENT_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index EMPLOYEES#_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "APP_DATA"."EMPLOYEES#_PK" ON "APP_DATA"."EMPLOYEES#" ("EMPLOYEE_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index JOB_HISTORY#_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "APP_DATA"."JOB_HISTORY#_PK" ON "APP_DATA"."JOB_HISTORY#" ("EMPLOYEE_ID", "START_DATE") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index JOBS#_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "APP_DATA"."JOBS#_PK" ON "APP_DATA"."JOBS#" ("JOB_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index DEPARTMENTS#_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "APP_DATA"."DEPARTMENTS#_PK" ON "APP_DATA"."DEPARTMENTS#" ("DEPARTMENT_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index DEPARTMENTS#_DM_NOT_NULL
--------------------------------------------------------

  CREATE UNIQUE INDEX "APP_DATA"."DEPARTMENTS#_DM_NOT_NULL" ON "APP_DATA"."DEPARTMENTS#" ("DEPARTMENT_NAME") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index EMPLOYEES#_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "APP_DATA"."EMPLOYEES#_PK" ON "APP_DATA"."EMPLOYEES#" ("EMPLOYEE_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index JOB_HISTORY#_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "APP_DATA"."JOB_HISTORY#_PK" ON "APP_DATA"."JOB_HISTORY#" ("EMPLOYEE_ID", "START_DATE") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index JOBS#_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "APP_DATA"."JOBS#_PK" ON "APP_DATA"."JOBS#" ("JOB_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Trigger EMPLOYEES#_AIUFER
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "APP_DATA"."EMPLOYEES#_AIUFER" 
AFTER INSERT OR UPDATE OF salary, job_id ON employees# FOR EACH ROW
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
AFTER UPDATE OF min_salary, max_salary ON jobs# FOR EACH ROW
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
AFTER INSERT OR UPDATE OF salary, job_id ON employees# FOR EACH ROW
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
AFTER UPDATE OF min_salary, max_salary ON jobs# FOR EACH ROW
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
