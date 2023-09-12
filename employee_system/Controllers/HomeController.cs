using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using employee_system.Models;
using employee_system.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace employee_system.Controllers;


public class HomeController : Controller
{


    private readonly ApplicationDbContext _dbContext;

    private readonly IEmailSender _emailSender;




    public HomeController(ApplicationDbContext dbContext, IEmailSender emailSender)
    {

        _dbContext = dbContext;

        _emailSender = emailSender;


    }




    public IActionResult employees()
    {
        var users = _dbContext.Employee_table.ToList();
        return View(users);

    }

    public IActionResult Announcements()
    {
        var announcements = _dbContext.Employee_table.ToList();
        return View(announcements);

    }


    public IActionResult Requests()
    {
        var requests = _dbContext.Request_table.ToList();
        return View(requests);

    }

    public IActionResult Tasks()
    {
        var tasks = _dbContext.Task_table.ToList();
        return View(tasks);

    }


    public IActionResult Index()
    {
        return View();
    }

    public IActionResult demote_page()
    {
        return View();
    }

    public IActionResult task_assign()
    {
        return View();
    }

    public IActionResult reset_pass_settings()
    {
        return View();
    }

    public IActionResult dashboard_leader()
    {
        return View();
    }


    public IActionResult promotion_page()
    {
        return View();
    }

    public IActionResult request_add()
    {
        return View();
    }



    public IActionResult reset_pass()
    {
        return View();
    }

    public IActionResult dashboard_hr()
    {
        return View();
    }

    public IActionResult employee_table()
    {
        return View();
    }

    public IActionResult request_table()
    {
        return View();
    }

    public IActionResult forgot_password()
    {
        return View();
    }


    public IActionResult page_register()
    {
        return View();
    }

    public IActionResult after_register()
    {
        return View();
    }

    public IActionResult Users_table()
    {
        return View();
    }


    public IActionResult Page_register_autopass()
    {
        return View();
    }

    public IActionResult main()
    {


        return View();
    }

    public IActionResult settings_profile()
    {
        return View();
    }


    public IActionResult assigned_tasks()
    {
        return View();
    }

    public IActionResult assigned_requests()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }




    [HttpGet]
    public IActionResult getProfileInfo(string username)
    {
        var profile_processed = _dbContext.Employee_table.FirstOrDefault(u => u.Username == username);

        return Json(profile_processed);

    }



    [HttpGet]
    public IActionResult getPassword(string username)
    {
        var profile_processed = _dbContext.Employee_table.FirstOrDefault(u => u.Username == username);

        return Json(profile_processed);

    }

    [HttpGet]
    public IActionResult GetUserCount()
    {
        var query = from employee in _dbContext.Employee_table
                    select employee;

        var num_employees = query.Count();

        var userCount = new
        {
            num = num_employees
        };

        return Ok(userCount);
    }

    [HttpGet]
    public IActionResult GetLeaderCount()
    {
        var query = from employee in _dbContext.Employee_table
                    where employee.Level == 2
                    select employee;

        var num_employees = query.Count();

        var userCount = new
        {
            num = num_employees
        };

        return Ok(userCount);

    }

    [HttpGet]
    public IActionResult GetTaskPercantage()
    {
        var query = from task in _dbContext.Task_table
                    where task.Status == "Completed"
                    select task;

        var completed_tasks = query.Count();

        var query_1 = from task in _dbContext.Task_table
                      where task.Status == "Pending"
                      select task;

        var pending_tasks = query_1.Count();

        var sum = completed_tasks + pending_tasks;

        double percantage = 0.0;
        if (sum != 0)
        {
            percantage = ((double)completed_tasks / sum) * 100;
        }

        string formattedPercentage = percantage.ToString("F1"); // Formated as a string with one decimal place

        var taskPercantage = new
        {
            num = formattedPercentage
        };

        return Ok(taskPercantage);
    }



    [HttpGet]
    public IActionResult GetRequestPercantage()
    {
        var query = from request in _dbContext.Request_table
                    where request.Status == "Accepted"
                    select request;

        var accepted_requests = query.Count();

        var query_1 = from request in _dbContext.Request_table
                      where request.Status == "Pending"
                      select request;

        var pending_requests = query_1.Count();

        var sum = accepted_requests + pending_requests;

        double percentage = 0.0;
        if (sum != 0)
        {
            percentage = ((double)accepted_requests / sum) * 100;
        }

        string formattedPercentage = percentage.ToString("F1"); // Format as a string with one decimal place

        var requestPercantage = new
        {
            num = formattedPercentage
        };

        return Ok(requestPercantage);
    }




    [HttpGet]
    public IActionResult getTasksByLeader_barchart(string cardName)
    {
        var leader_processed = _dbContext.Employee_table.FirstOrDefault(u => u.Username == cardName);
        var department_processed = leader_processed.Department;

        var employeesInDepartment = _dbContext.Employee_table.Where(e => e.Department == department_processed).ToList();

        var tasksByEmployee = new Dictionary<Employee, List<Models.Task>>();

        foreach (var employee in employeesInDepartment)
        {
            // Get tasks assigned to the current employee
            var tasks = _dbContext.Task_table
                .Where(t => t.Assigned_To == employee.Id)
                .ToList();

            tasksByEmployee.Add(employee, tasks);
        }

        // Now you have a dictionary where the key is the employee and the value is their list of tasks

        // Prepare data for the chart
        var employeeNames = new List<string>();
        var completedTasks = new List<int>();
        var pendingTasks = new List<int>();

        foreach (var kvp in tasksByEmployee)
        {
            var employee = kvp.Key;
            var tasks = kvp.Value;

            employeeNames.Add($"{employee.First_Name} {employee.Last_Name}");

            // Count completed and pending tasks
            var completedCount = tasks.Count(t => t.Status == "Completed");
            var pendingCount = tasks.Count(t => t.Status == "Pending");

            completedTasks.Add(completedCount);
            pendingTasks.Add(pendingCount);
        }

        // Create a JavaScript object with the chart data
        var chartData = new
        {
            labels = employeeNames,
            datasets = new[]
            {
        new
        {
            label = "Completed",
            data = completedTasks,
            backgroundColor = "green"
        },
        new
        {
            label = "Pending",
            data = pendingTasks,
            backgroundColor = "orange"
        }
    }
        };

        // Return the chart data as JSON
        return Json(chartData);
    }


    [HttpGet]
    public IActionResult LoadChart(string cardName)
    {
        return View(); // Create a view for loading the chart data via AJAX
    }



    [HttpGet]
    public IActionResult getTasks_barchart(string cardName)
    {
        var query = from employee in _dbContext.Employee_table
                    select employee;

        var employeesInPortal = query.ToList();


        var tasksByEmployee = new Dictionary<Employee, List<Models.Task>>();

        foreach (var employee in employeesInPortal)
        {
            // Get tasks assigned to the current employee
            var tasks = _dbContext.Task_table
                .Where(t => t.Assigned_To == employee.Id)
                .ToList();

            tasksByEmployee.Add(employee, tasks);
        }

        // Now you have a dictionary where the key is the employee and the value is their list of tasks

        // Prepare data for the chart
        var employeeNames = new List<string>();
        var completedTasks = new List<int>();
        var pendingTasks = new List<int>();

        foreach (var kvp in tasksByEmployee)
        {
            var employee = kvp.Key;
            var tasks = kvp.Value;

            employeeNames.Add($"{employee.First_Name} {employee.Last_Name}");

            // Count completed and pending tasks
            var completedCount = tasks.Count(t => t.Status == "Completed");
            var pendingCount = tasks.Count(t => t.Status == "Pending");

            completedTasks.Add(completedCount);
            pendingTasks.Add(pendingCount);
        }

        // Create a JavaScript object with the chart data
        var chartData = new
        {
            labels = employeeNames,
            datasets = new[]
            {
        new
        {
            label = "Completed",
            data = completedTasks,
            backgroundColor = "green"
        },
        new
        {
            label = "Pending",
            data = pendingTasks,
            backgroundColor = "orange"
        }
    }
        };

        // Return the chart data as JSON
        return Json(chartData);
    }


 

    // Calculate the percentage of completed tasks for a specific department
    [HttpGet]
    public IActionResult getTasksByLeader_pie(string cardName)
    {
        var query = from task in _dbContext.Task_table
                    join employee in _dbContext.Employee_table
                    on task.Assigned_By equals employee.Id
                    where task.Status == "Completed" && employee.Username == cardName
                    select task;

        var completedTasks = query.Count();

        query = from task in _dbContext.Task_table
                join employee in _dbContext.Employee_table
                on task.Assigned_By equals employee.Id
                where employee.Username == cardName
                select task;

        var totalTasks = query.Count();

        var percentageCompleted = totalTasks > 0 ? (double)completedTasks / totalTasks * 100 : 0;

        var percentageData = new
        {
            CompletedPercentage = percentageCompleted,
            PendingPercentage = 100 - percentageCompleted
        };

        return Json(percentageData);
    }



    [HttpPost]
    public IActionResult CreateEmployee(Employee employee)
    {
        var employee_proccessed = _dbContext.Employee_table.FirstOrDefault(u => u.Username == employee.Username);




        if (employee_proccessed == null)
        {

            _dbContext.Employee_table.Add(employee);
            _dbContext.SaveChanges();
            return Content("User added Successfuly");
        }

        else
        {
            return Content("User Already taken");
        }

    }





    [HttpPost]
    public IActionResult CreateAnnouncement(Announcement announcement)
    {
        var announcement_proccessed = _dbContext.Announcement_table.FirstOrDefault(u => u.Details == announcement.Details);




        if (announcement_proccessed == null)
        {

            _dbContext.Announcement_table.Add(announcement);
            _dbContext.SaveChanges();
            return Content("Announcment added Successfuly");
        }

        else
        {
            return Content("Announcment already added");
        }

    }




    [HttpPost]
    public IActionResult CreateRequest(Request request)
    {
        var request_proccessed = _dbContext.Request_table.FirstOrDefault(u => u.Id == request.Id);




        if (request_proccessed == null)
        {

            _dbContext.Request_table.Add(request);
            _dbContext.SaveChanges();
            return Content("Request added Successfuly");
        }

        else
        {
            return Content("Request already added");
        }

    }



    [HttpPost]
    public IActionResult CreateTask(Models.Task task)
    {
        var task_proccessed = _dbContext.Task_table.FirstOrDefault(u => u.Id == task.Id);




        if (task_proccessed == null)
        {

            _dbContext.Task_table.Add(task);
            _dbContext.SaveChanges();
            return Content("Task added Successfuly");
        }

        else
        {
            return Content("Task already added");
        }

    }





    [HttpPost]
    public IActionResult Authenticate(string username, string pass)
    {
        var user = _dbContext.Employee_table.FirstOrDefault(u => u.Username == username);
        if (user == null)
        {
            return Content("User not found");
        }

        if (pass == user.Password)
        {
            return Content("Authentication successful");
        }

        return Content("Authentication failed");
    }


    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _dbContext.Employee_table.Select(u => new
        {
            u.Id,
            u.Username,
            u.First_Name,
            u.Last_Name,
            u.Department,
            u.Phone_Number,
            u.Level


        }).ToList();

        return Json(users);
    }

    [HttpGet]
    public IActionResult GetLevel1Employees(string cardName)
    {

        var team_leader = _dbContext.Employee_table.FirstOrDefault(u => u.Username == cardName);

        var leader_department = team_leader.Department;



        var employees = _dbContext.Employee_table
            .Where(e => e.Department == leader_department && e.Level == 1)
            .Select(e => new
            {
                e.Id,
                e.Username,
                e.Last_Name

            })
            .ToList();

        return Json(employees);
    }




    [HttpGet]
    public IActionResult getAnnouncements()
    {
        var announcements = _dbContext.Announcement_table.Select(u => new
        {
            u.Subject,
            u.Date,
            u.Details

        }).ToList();

        return Json(announcements);
    }


    [HttpGet]
    public IActionResult getRequests()
    {
        var requests = _dbContext.Request_table.Select(u => new
        {
            u.Id,
            u.Requested_By,
            u.Employee_1.Username,
            u.Subject,
            u.Date,
            u.Details,
            u.Status,
            u.Change

        }).ToList();

        return Json(requests);
    }


    [HttpGet]
    public IActionResult getTasks(int emp_id)
    {


        var query = from task in _dbContext.Task_table
                    where task.Assigned_To == emp_id
                    select new
                    {
                        task.Id,
                        task.Subject,
                        task.Due_Date,
                        task.Details,
                        task.Status
                    };

        var tasksofEmployee = query.ToList();



        return Json(tasksofEmployee);
    }



    [HttpGet]
    public IActionResult GetEmployeeTaskStatus(string cardName)
    {
        var query = from task in _dbContext.Task_table
                    join employee in _dbContext.Employee_table
                    on task.Assigned_To equals employee.Id
                    where employee.Username == cardName
                    select new
                    {
                        task.Id,
                        employee.Username,
                        task.Status
                    };


        var tasksWithStatus = query.ToList();

        return Json(tasksWithStatus);
    }



    [HttpGet]
    public IActionResult GetTasksByLeader(string username)
    {

        var query = from task in _dbContext.Task_table
                    join employee in _dbContext.Employee_table
                    on task.Assigned_To equals employee.Id
                    where employee.Username == username
                    select new
                    {
                        task.Id,
                        employee.Username,
                        task.Subject,
                        task.Due_Date,
                        task.Details,
                        task.Status
                    };

        var tasks_list = query.ToList();

        return Json(tasks_list);
    }


    [HttpPost]
    public IActionResult GetTasksSpecific(string username)
    {



        var query = from task in _dbContext.Task_table
                    join employee in _dbContext.Employee_table
                    on task.Assigned_To equals employee.Id
                    where employee.Username == username
                    select new
                    {
                        task.Id,
                        task.Assigned_To,
                        task.Assigned_By,
                        task.Subject,
                        task.Details,
                        task.Due_Date,
                        task.Status
                    };

        var task_list = query.ToList();



        if (task_list != null)
        {
            return Json(task_list);
        }
        else
        {
            return NotFound("Request not found");
        }
    }




    [HttpPost]
    public IActionResult GetRequestsSpecific(string username)

    {

        var query = from request in _dbContext.Request_table
                    join employee in _dbContext.Employee_table
                    on request.Requested_By equals employee.Id
                    where employee.Username == username
                    select new
                    {
                        request.Subject,
                        request.Details,
                        request.Date,
                        request.Status
                    };

        var request_list = query.ToList();


        if (request_list != null)
        {
            return Json(request_list);
        }
        else
        {
            return NotFound("Request not found");
        }
    }


    [HttpPost]

    public async Task<IActionResult> promote(string email)
    {

        var user_proccessed = _dbContext.Employee_table.FirstOrDefault(u => u.Username == email);


        if (user_proccessed == null)
        {

            return Content("User doesnt exist");
        }

        else
        {


            user_proccessed.Level = user_proccessed.Level + 1;
            _dbContext.Update(user_proccessed);
            await _dbContext.SaveChangesAsync();

            var sirName = user_proccessed.Last_Name;
            string subject = "Important:  Request Information";
            string message = "Dear " + sirName + ",\n\nYour request has been succesfully accepted.";

            await _emailSender.SendEmailAsync(email, subject, message);



            return Content("Request accepted successfully");
        }
    }



    [HttpPost]

    public async Task<IActionResult> demote(string email)
    {

        var user_proccessed = _dbContext.Employee_table.FirstOrDefault(u => u.Username == email);


        if (user_proccessed == null)
        {

            return Content("User doesnt exist");
        }

        else
        {


            user_proccessed.Level = user_proccessed.Level - 1;
            _dbContext.Update(user_proccessed);
            await _dbContext.SaveChangesAsync();

            var sirName = user_proccessed.Last_Name;
            string subject = "Important:  Request Information";
            string message = "Dear " + sirName + ",\n\nYou have been revoked of your Team Leader Status";

            await _emailSender.SendEmailAsync(email, subject, message);



            return Content("Request accepted successfully");
        }
    }


    [HttpPost]
    public async Task<IActionResult> task_completed(string email, int id, int emp_id, int leader_id)
    {

        var taskProcessed = _dbContext.Task_table
    .Include(t => t.Employee_1)
    .Include(t => t.Employee_2)
    .FirstOrDefault(u => u.Id == id);
        var empProcessed = taskProcessed?.Employee_2;
        var leaderProcessed = taskProcessed?.Employee_1;

        if (taskProcessed == null)
        {
            return Content("Task doesn't exist");
        }

        else
        {

            taskProcessed.Status = "Completed";
            _dbContext.Update(taskProcessed);
            await _dbContext.SaveChangesAsync();

            var sirName = empProcessed.First_Name;

            string subject = "Important: Task Information";
            string message = "Dear " + sirName + ",\n\nYour task has been successfully registered as completed. Team leader will be notified by email";


            var sirName_1 = empProcessed.First_Name;

            string subject_1 = "Important: Task Information";
            string message_1 = "Dear " + sirName + ",\n\n The  task you assigned to" + sirName + " has been  completed. You can review his work now";

            var email_1 = leaderProcessed.Username;

            await _emailSender.SendEmailAsync(email, subject, message);

            await _emailSender.SendEmailAsync(email_1, subject_1, message_1);


            return Content("task completed successfully");
        }
    }




    [HttpPost]
    public async Task<IActionResult> request_accepted(string email, int id, int user_id)
    {
        // Check if there is a request with the given email

        var requestProcessed = _dbContext.Request_table.FirstOrDefault(u => u.Id == id);
        var userProcessed = _dbContext.Employee_table.FirstOrDefault(u => u.Id == user_id);


        if (requestProcessed == null)
        {
            return Content("Request doesn't exist");
        }
        else
        {

            if (requestProcessed.Subject == "Department Change")
            {
                userProcessed.Department = requestProcessed.Change;
                requestProcessed.Status = "Accepted";

                _dbContext.Update(requestProcessed);

            }
            else
            {


                requestProcessed.Status = "Accepted";
                _dbContext.Update(requestProcessed);
            }

            await _dbContext.SaveChangesAsync();

            var sirName = userProcessed.Last_Name;
            string subject = "Important:  Request Information";
            string message = "Dear " + sirName + ",\n\nYour request has been succesfully accepted.";

            await _emailSender.SendEmailAsync(email, subject, message);



            return Content("Request accepted successfully");
        }


    }




    [HttpPost]

    public async Task<IActionResult> request_denied(string email)
    {

        var request_processed = _dbContext.Request_table
        .Include(task => task.Employee_1)
        .FirstOrDefault(task => task.Employee_1.Username == email);


        if (request_processed == null)
        {

            return Content("Request doesnt exist");
        }

        else
        {
            request_processed.Status = "Denied";
            _dbContext.Update(request_processed);
            await _dbContext.SaveChangesAsync();

            var sirName = request_processed.Employee_1.Last_Name;
            string subject = "Important:  Request Information";
            string message = "Dear " + sirName + ",\n\nYour request has been  denied;  We apologise for any misunderstandings, if you want to protest the following decision you can book an appointment with HR and ask them for the reasons for denying the request";

            await _emailSender.SendEmailAsync(email, subject, message);



            return Content("Request denied");
        }


    }


    [HttpGet]
    public IActionResult getNames(string username)

    {
        var user = _dbContext.Employee_table.FirstOrDefault(u => u.Username == username);
        if (user == null)
        {
            return Content("User not found");
        }
        else
        {
            var userName = new { user.First_Name, user.Last_Name, user.Id, user.Department, user.Phone_Number, user.Username };
            return Json(userName);
        }
    }



    [HttpPost]
    public IActionResult getLevel(string username)

    {
        var user = _dbContext.Employee_table.FirstOrDefault(u => u.Username == username);
        if (user == null)
        {
            return Content("User not found");
        }
        else
        {

            if (user.Level == 3)
            {


                return Content("Level_3");
            }

            else if (user.Level == 2)
            {
                return Content("Level_2");

            }
            else
            {
                return Content("Level_1");
            }
        }
    }


    [HttpPost]
    public async Task<IActionResult> forgetpassword_email(string email, string verification)
    {

        var user_proccessed = _dbContext.Employee_table.FirstOrDefault(u => u.Username == email);


        if (user_proccessed == null)
        {

            return Content("User doesnt exist");
        }

        else
        {


            var sirName = user_proccessed.Last_Name;

            string subject = "Important: Your Account Information";

            string message = "Dear " + sirName + ",\n\nThank you for registering with us. We are pleased to provide you with your verification code details:\n Verification code: " + verification + "\n\n To complete the password  resit, please enter the following verification code. If you have any questions or need further assistance, please don't hesitate to contact our support team.\n\nBest regards,\nAhmed Hassanin";

            await _emailSender.SendEmailAsync(email, subject, message);

            // Store the email in a session
            HttpContext.Session.SetString("resetEmail", email);

            return Content("Email succesfully sent");
        }


    }

    [HttpPost]

    public async Task<IActionResult> reset_pass_email(string Pass)
    {

        // Retrieve the email from the session
        var email = HttpContext.Session.GetString("resetEmail");


        var user_proccessed = _dbContext.Employee_table.FirstOrDefault(u => u.Username == email);


        if (user_proccessed == null)
        {

            return Content("User doesnt exist");
        }

        else
        {
            user_proccessed.Password = Pass;
            _dbContext.Update(user_proccessed);
            await _dbContext.SaveChangesAsync();

            var sirName = user_proccessed.Last_Name;
            string subject = "Important: Your Account Information";
            string message = "Dear " + sirName + ",\n\nYour password has been successfully changed.";

            await _emailSender.SendEmailAsync(email, subject, message);

            // Remove the email from the session after use
            HttpContext.Session.Remove("resetEmail");

            return Content("Email successfully sent and password updated.");
        }


    }



    [HttpPost]

    public async Task<IActionResult> reset_pass_email_settings(string Pass, string email)
    {




        var user_proccessed = _dbContext.Employee_table.FirstOrDefault(u => u.Username == email);


        if (user_proccessed == null)
        {

            return Content("User doesnt exist");
        }

        else
        {
            user_proccessed.Password = Pass;
            _dbContext.Update(user_proccessed);
            await _dbContext.SaveChangesAsync();

            var sirName = user_proccessed.Last_Name;
            string subject = "Important: Your Account Information";
            string message = "Dear " + sirName + ",\n\nYour password has been successfully changed.";

            await _emailSender.SendEmailAsync(email, subject, message);

            // Remove the email from the session after use
            HttpContext.Session.Remove("resetEmail");

            return Content("Email successfully sent and password updated.");
        }


    }


    [HttpPost]
    public async Task<IActionResult> autoopass(string email, string name, string sirName, string Pass, string department, int level, string num)
    {

        var newUser = new Employee
        {
            Username = email,
            Password = Pass,
            First_Name = name,
            Last_Name = sirName,
            Department = department,
            Phone_Number = num,
            Level = level

        };


        var user_proccessed = _dbContext.Employee_table.FirstOrDefault(u => u.Username == newUser.Username);

        if (user_proccessed == null)
        {

            string subject = "Important: Your Account Information";

            string message = "Dear " + sirName + ",\n\nThank you for registering with us. We are pleased to provide you with your account details:\n\nUsername: " + email + "\nPassword: " + Pass + "\n\nTo complete the registration process, please log in using the provided credentials. If you have any questions or need further assistance, please don't hesitate to contact our support team.\n\nBest regards,\nAhmed Hassanin";

            await _emailSender.SendEmailAsync(email, subject, message);

            _dbContext.Employee_table.Add(newUser);
            _dbContext.SaveChanges();


            return Content("ok");
        }

        else
        {

            return Content("User already Taken");

        }

    }


}






