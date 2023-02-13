---------------
-- Load data --
---------------

PROMPT
PROMPT Specify password for app_data as parameter 1:
DEFINE pass_app_data = &1

CONNECT app_data/&pass_app_data

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
