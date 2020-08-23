﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace GoalsManager.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GoalsController : Controller
	{
		private readonly GoalContext _goalContext;

		public GoalsController(GoalContext goalContext)
		{
			_goalContext = goalContext;
		}

		[HttpGet("/")]
		[HttpGet("/[controller]", Name = "GoalsView")]
		public IActionResult Index()
		{
			// TODO route view to Views/Home/Index.cshtml
			return View(GetAllGoals());
		}

		[HttpGet("/[controller]/add", Name = "AddGoalView")]
		public IActionResult AddGoal()
		{
			return View();
		}

		[HttpPost("/[controller]/add", Name = "AddGoalAction")]
		public IActionResult AddGoalAction([FromForm] Goal goal)
		{
			AddGoal(goal);
			return View("AddGoal");
		}

		[HttpGet("/[controller]/{id}/edit", Name = "EditGoalView")]
		public IActionResult EditGoal(long id)
		{
			return View(GetGoalById(id));
		}

		[HttpPost("/[controller]/{id}/edit", Name = "EditGoalAction")]
		public IActionResult EditGoalAction([FromForm] Goal goal)
		{
			UpdateGoal(goal);
			return View("EditGoal", goal);
		}

		[HttpGet("/[controller]/{id}/delete", Name = "DeleteGoalView2")]
		public IActionResult DeleteGoal(long id)
		{
			return View(GetGoalById(id));
		}

		[HttpPost("/[controller]/{id}/delete", Name = "DeleteGoalAction")]
		public IActionResult DeleteGoalAction([FromForm] Goal goal)
		{
			DeleteGoalById(goal.GoalId);
			return RedirectToAction("Index");
		}

		#region Goal CRUD operations
		[HttpPost("AddGoal")]
		public long AddGoal(Goal goal)
		{
			_goalContext.Add(goal);
			_goalContext.SaveChanges();

			return goal.GoalId;
		}

		[HttpPut("UpdateGoal")]
		public void UpdateGoal(Goal goal)
		{
			var currentGoal = _goalContext.Goals.Find(goal.GoalId);

			if (currentGoal != null)
			{
				_goalContext.Entry(currentGoal).CurrentValues.SetValues(goal);
				_goalContext.SaveChanges();
			}
		}

		[HttpDelete("DeleteGoal")]
		public void DeleteGoalById(long goalId)
		{
			var currentGoal = _goalContext.Goals.Find(goalId);

			if (currentGoal != null)
			{
				_goalContext.Remove(currentGoal);
				_goalContext.SaveChanges();
			}
		}

		[HttpGet("GetGoalById")]
		public Goal GetGoalById(long goalId)
		{
			return _goalContext.Goals.Find(goalId);
		}

		[HttpGet("GetAllGoals")]
		public IEnumerable<Goal> GetAllGoals()
		{
			return _goalContext.Goals;
		}
		#endregion

		#region Notification CRUD operations
		[HttpPost("{goalId}/AddNotification")]
		public long AddNotification(long goalId, Notification notification)
		{
			notification.GoalId = goalId;
			_goalContext.Add(notification);
			_goalContext.SaveChanges();

			return notification.NotificationId;
		}

		[HttpPut("UpdateNotification")]
		public void UpdateNotification(Notification notification)
		{
			var currentNotification = _goalContext.Notifications.Find(notification.NotificationId);

			if (currentNotification != null)
			{
				_goalContext.Entry(currentNotification).CurrentValues.SetValues(notification);
				_goalContext.SaveChanges();
			}
		}

		[HttpDelete("DeleteNotification")]
		public void DeleteNotification(long notificationId)
		{
			var currentNotification = _goalContext.Notifications.Find(notificationId);

			if (currentNotification != null)
			{
				_goalContext.Remove(currentNotification);
				_goalContext.SaveChanges();
			}
		}

		[HttpGet("{goalId}/GetNotificationsByGoalId")]
		public IEnumerable<Notification> GetNotificationsByGoalId(long goalId)
		{
			return _goalContext.Notifications.Where(el => el.GoalId == goalId);
		}
		#endregion
	}
}