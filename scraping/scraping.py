from selenium import webdriver
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.chrome.service import Service
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.common.action_chains import ActionChains
import selenium.common
from webdriver_manager.chrome import ChromeDriverManager
import time
import re
import csv
import pandas as pd

# Locators
# Programmes and Courses
programmes_loc = "(//div[@class='panel-body'])[7]//a"
sit_loc = "//a[normalize-space()='Software Engineering and Information Technologies']"
courses_loc = "//a[contains(@id, 'viewPredmetA')]"
predmeti_prof_loc = "//table[@id='planTable']//a"
duration_loc = '//h4[.//small[text() = \'Duration (year/sem)\']]'
ects_loc = '//h4[.//small[text() = \'Total ECTS\']]'
close_btn_loc = "//button[@data-dismiss='modal']"
chief_loc = "//div[@id='odgovorniTab-1']//h4/a"

# course_spec_ui_loc = "//ul[@class='nav nav-tabs']//li//a"
course_spec_ui_loc = "//div[@class='panel-body']//div[@class='panel-body']//li//a"
ppTabs_one_title_loc = "//a[normalize-space()='Basic informations']"
ects_course_loc = '//tr/td[text() = "ECTS"]/following-sibling::td'
goal_title_loc = "//a[contains(@href, \'ppTabs-2\')]"
goal_text_loc = "//div[@id='ppTabs-2']"
educational_outcomes_title_loc = "//a[contains(@href, \'ppTabs-3\')]"
educational_outcomes_text_loc = "//div[@id='ppTabs-3']"
lectures_loc = "//a[normalize-space()='Lecturers']"
professors_loc = "//div[@id='ppTabs-9']//a"
professors_mail_loc = "//table[@class='table table-hover table-fixer']//a"

#Waitings locators
general_info_loc = "//div[@id='konteksniLinkoviGroup']//a[contains(text(),'General information')]"

# Options
options = Options()
options.add_argument("start-maximized")
# options.add_argument("headless")

driver = webdriver.Chrome(service=Service(r"C:\Chrome driver\chromedriver.exe"), options=options)
url = 'http://ftn.uns.ac.rs/963620642/faculty-of-technical-sciences'
driver.get(url)


# Methods
def find_element(element):
    try:
        driver.find_element(By.XPATH, element).click()
    except selenium.common.exceptions.ElementNotInteractableException:
        None


def find_element_without_click(xpath):
    try:
        element = driver.find_element(By.XPATH, xpath)
        return element
    except (
    selenium.common.exceptions.ElementNotInteractableException, selenium.common.exceptions.NoSuchElementException):
        None


def find_elements(xpath):
    try:
        elements = driver.find_elements(By.XPATH, xpath)
        return elements
    except selenium.common.exceptions.ElementNotInteractableException:
        print("null")


def button_click(button):
    try:
        button.click()
    except selenium.common.exceptions.ElementNotInteractableException:
        None


def open_link_new_tab(button):
    try:
        button.send_keys(Keys.CONTROL + Keys.RETURN)
    except selenium.common.exceptions.ElementNotInteractableException:
        None


def close_tab():
    try:
        driver.find_element(By.TAG_NAME, 'body').send_keys(Keys.CONTROL + 'w')
    except selenium.common.exceptions.ElementNotInteractableException:
        None


programmes = find_elements(programmes_loc)

print("*" * 10, "Programmes", "*" * 10)
programmes_list = []
programme_url = []
duration_list = []
ects_list = []
chief_list = []
for programme in programmes:
    print(programme.text)
    programmes_list.append(programme.text)
    programme_url.append(programme.get_attribute('href'))
    curWindowHndl = driver.current_window_handle
    open_link_new_tab(programme)
    try:
        driver.switch_to.window(driver.window_handles[1])
        driverStr = str(driver.current_url)
        time.sleep(3)
        duration = find_element_without_click(duration_loc)
        duration_list.append(duration.text)
        ects = find_element_without_click(ects_loc)
        ects_list.append(ects.text)
        chief = find_element_without_click(chief_loc)
        if chief:
            print(chief.text)
            chief_list.append(chief.text)
        else:
            print("null")
            chief_list.append("null")
        driver.close()
        driver.switch_to.window(curWindowHndl)
    except IndexError:
        continue

# programmes csv data
programmes_dict = {
            'Programmes': programmes_list,
            'URL': programme_url,
            'Duration': duration_list,
            'ECTS': ects_list,
            'Chief': chief_list
          }

dt = pd.DataFrame.from_dict(programmes_dict)
dt.transpose()

# open the file in the write mode
print(dt)
dt.to_csv('programmes.csv', encoding='UTF8', na_rep='null', sep=',', index=False)


driver.find_element(By.XPATH, sit_loc).click()

courses = driver.find_elements(By.XPATH, courses_loc)
course_spec_ui = find_elements(course_spec_ui_loc)

print("*" * 10, "Courses", "*" * 10)
courses_list = []
course_url = []
ects_course_list = []
goal_list = []
educational_outcomes_list = []
professors_list = []
professors_url = []
professors_mail_list = []

for course in courses:
    num = len(course.text)
    if num > 0:
        if "Elective" not in course.text:
            course_without_elective = str(course.text)
            print(course_without_elective)
            courses_list.append(course_without_elective)
            course_url.append(course.get_attribute('href'))

    curWindowHndl = driver.current_window_handle
    open_link_new_tab(course)
    find_element(close_btn_loc)
    try:
        driver.switch_to.window(driver.window_handles[1])
        driverStr = str(driver.current_url)
        #time.sleep(3)
        WebDriverWait(driver, 3).until(EC.presence_of_element_located((By.XPATH, general_info_loc)))
        ectsCourse = find_element_without_click(ects_course_loc)
        print("CourseECTS", ectsCourse.text)
        ects_course_list.append(ectsCourse.text)
        goal_title = find_elements(goal_title_loc)
        goal_text = find_element_without_click(goal_text_loc)
        print("Educational goal", goal_text.get_attribute("textContent"))
        goal_list.append(goal_text.get_attribute("textContent"))
        educational_outcomes_title = find_elements(educational_outcomes_title_loc)
        educational_outcomes_text = find_element_without_click(educational_outcomes_text_loc)
        educational_splited = re.sub('\n', ' ', educational_outcomes_text.get_attribute("textContent"))
        print("Educational outcomes", educational_splited)
        educational_outcomes_list.append(educational_splited)
        professors = find_elements(professors_loc)
        # print(len(professors))
        if len(professors) > 1:
            profesor = professors[1].get_attribute("textContent")
            profesor_splited = re.sub(' +', ' ', profesor)
            print(profesor_splited)
            #print(professors[1].get_attribute("textContent"))
            professors_list.append(profesor_splited)
            professors_url.append(professors[1].get_attribute('href'))
            find_element(lectures_loc)
            #time.sleep(3)
            WebDriverWait(driver, 3).until(EC.visibility_of_element_located((By.XPATH, professors_loc)))

            courseWindowHndl = driver.current_window_handle
            open_link_new_tab(professors[1])
            driver.switch_to.window(driver.window_handles[2])
            time.sleep(2)
            professors_mail = find_element_without_click(professors_mail_loc)
            print(professors_mail.text)
            professors_mail_list.append(professors_mail.text)
            driver.close()
            driver.switch_to.window(courseWindowHndl)
            time.sleep(1)

            driver.close()
            driver.switch_to.window(curWindowHndl)
        else:
            professors_list.append("null")
            professors_url.append("null")
            professors_mail_list.append("null")
            time.sleep(1)
            driver.close()
            driver.switch_to.window(curWindowHndl)
        time.sleep(2)
    except IndexError:
        continue

listes = [courses_list, course_url, ects_course_list, goal_list, educational_outcomes_list,
          professors_list, professors_url, professors_mail_list]

for lista in listes:
    print(str(lista))
    while len(lista) < 31:
        lista.append("null")

# csv data
course_dict = {
            'Course': courses_list,
            'CourseURL': course_url,
            'CourseECTS': ects_course_list,
            'Goals': goal_list,
            'Outcomes': educational_outcomes_list,
            'Professors': professors_list,
            'ProfessorsURL': professors_url,
            'ProfessorsMail': professors_mail_list
          }

dt = pd.DataFrame.from_dict(course_dict)
dt.transpose()

# open the file in the write mode
print(dt)
dt.to_csv('courses.csv', index=False, encoding='UTF8', sep=',')


user_choice = input('Please click ENTER button to close application')
if not user_choice:
    print("ABORTED")
    quit()
