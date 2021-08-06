using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using HotelManagement.API.Infrastructure;
using HotelManagement.API.Models;
using Microsoft.AspNet.Identity;

namespace HotelManagement.API.Controllers
{
	[RoutePrefix("api/account")]
	public class AccountController : BaseApiController
    {
	    [AllowAnonymous]
	    [Route("init")]
	    [HttpGet]
	    public IHttpActionResult InitDb()
	    {
		    this.AppUserManager.Users.ToList().Select(u => this.TheModelFactory.Create(u));
		    return Ok();
	    }

		[Authorize(Roles = "Admin")]
		[Route("")]
	    [HttpGet]
	    public IHttpActionResult GetUsers()
	    {
		    return Ok(this.AppUserManager.Users.ToList().Select(u => this.TheModelFactory.Create(u)));
	    }

	    [Authorize(Roles = "Admin")]
		[Route("{id:guid}", Name = "GetUserById")]
		[HttpGet]
	    public async Task<IHttpActionResult> GetUser(string Id)
	    {
		    var user = await this.AppUserManager.FindByIdAsync(Id);

		    if (user != null)
		    {
			    return Ok(this.TheModelFactory.Create(user));
		    }

		    return NotFound();

	    }

		[AllowAnonymous]
	    [Route("Register")]
	    [HttpPost]
	    public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
	    {
		    if (!ModelState.IsValid)
		    {
			    return BadRequest(ModelState);
		    }

		    var user = new ApplicationUser()
		    {
				FirstName = createUserModel.FirstName,
				LastName = createUserModel.LastName,
				UserName = createUserModel.Email,
				EmailConfirmed = true,
			    Email = createUserModel.Email
		    };

		    IdentityResult addUserResult = await this.AppUserManager.CreateAsync(user, createUserModel.Password);

		    if (!addUserResult.Succeeded)
		    {
			    return GetErrorResult(addUserResult);
		    }

		    Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

		    return Created(locationHeader, TheModelFactory.Create(user));
	    }

	    [Authorize(Roles = "Admin")]
	    [Route("{id:guid}/roles")]
	    [HttpPut]
	    public async Task<IHttpActionResult> AssignRolesToUser([FromUri] string id, [FromBody] string[] rolesToAssign)
	    {
		    var appUser = await this.AppUserManager.FindByIdAsync(id);

		    if (appUser == null)
		    {
			    return NotFound();
		    }

		    var currentRoles = await this.AppUserManager.GetRolesAsync(appUser.Id);

		    var rolesNotExists = rolesToAssign.Except(this.AppRoleManager.Roles.Select(x => x.Name)).ToArray();

		    if (rolesNotExists.Any())
		    {
			    ModelState.AddModelError("", $"Roles '{string.Join(",", rolesNotExists)}' does not exists in the system");
			    return BadRequest(ModelState);
		    }

		    IdentityResult removeResult = await this.AppUserManager.RemoveFromRolesAsync(appUser.Id, currentRoles.ToArray());

		    if (!removeResult.Succeeded)
		    {
			    ModelState.AddModelError("", "Failed to remove user roles");
			    return BadRequest(ModelState);
		    }

		    IdentityResult addResult = await this.AppUserManager.AddToRolesAsync(appUser.Id, rolesToAssign);

		    if (!addResult.Succeeded)
		    {
			    ModelState.AddModelError("", "Failed to add user roles");
			    return BadRequest(ModelState);
		    }

		    return Ok();
	    }

	    [Authorize(Roles = "Admin")]
		[Route("{id:guid}")]
		[HttpDelete]
	    public async Task<IHttpActionResult> DeleteUser(string id)
	    {
			var appUser = await this.AppUserManager.FindByIdAsync(id);

			if (appUser != null)
			{
				IdentityResult result = await this.AppUserManager.DeleteAsync(appUser);

				if (!result.Succeeded)
				{
					return GetErrorResult(result);
				}

				return Ok();

			}

			return NotFound();
	    }

	    [Authorize]
		[Route("ChangePassword")]
	    [HttpPut]
		public async Task<IHttpActionResult> ChangePassword(ChangeUserPasswordBindingModel changeUserPasswordModel)
	    {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			IdentityResult result = await this.AppUserManager.ChangePasswordAsync(User.Identity.GetUserId(), changeUserPasswordModel.CurrentPassword, changeUserPasswordModel.NewPassword);

			if (!result.Succeeded)
			{
				return GetErrorResult(result);
			}

			return Ok();
		}
	}
}
