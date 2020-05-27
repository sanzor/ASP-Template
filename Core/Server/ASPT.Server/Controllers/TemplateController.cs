using ASPT.Conventions;
using ASPT.Interfaces;
using ASPT.Models;
using ASPT.Routes;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPT.Server.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class TemplateController:Controller {
        private ILogger log = Log.ForContext<TemplateController>();
        private IUserService userService;
        public TemplateController(IUserService userService) => this.userService = userService;

        [Route(Routes.Routes.GETALL)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetByIdAsync() {
            using (LogContext.PushProperty(Constants.CORELLATION_ID, Guid.NewGuid().ToString())) {
                try {
                    
                    var users = await this.userService.GetUsersAsync();
                    return Ok(users);
                } catch (Exception ex) {
                    return StatusCode(500, ex.Message);

                }
            }
        }
        [Route(Routes.Routes.GET_BY_ID)]
        [HttpGet]
        public async Task<ActionResult<User>> GetByIdAsync(int ?id) {
            using (LogContext.PushProperty(Constants.CORELLATION_ID, Guid.NewGuid().ToString())) {
                try {
                    if (!id.HasValue) {
                        return StatusCode(400, "No Id provided in querystring");
                    }
                    var user = await this.userService.GetByIdAsync(id.Value);
                    return Ok(user);
                } catch (Exception ex) {
                    return StatusCode(500, ex.Message);
                    
                }
            }
        }
        [Route(Routes.Routes.INSERT)]
        [HttpPost]
        public async Task<ActionResult<User>> InsertAsync(User newUser) {
            using (LogContext.PushProperty(Constants.CORELLATION_ID, Guid.NewGuid().ToString())) {
                try {
                    if (newUser==null) {
                        return StatusCode(400, "Invalid user provided");
                    }
                    var user = await this.userService.InsertAsync(newUser);
                    return Ok(user);
                } catch (Exception ex) {
                    return StatusCode(500, ex.Message);

                }
            }
        }
        [Route(Routes.Routes.UPDATE)]
        [HttpPatch]
        public async Task<ActionResult<User>> UpdateAsync(User updatedUser) {
            using (LogContext.PushProperty(Constants.CORELLATION_ID, Guid.NewGuid().ToString())) {
                try {
                    if (updatedUser == null) {
                        return StatusCode(400, "Invalid user provided");
                    }
                    var user = await this.userService.UpdateAsync(updatedUser);
                    return Ok(user);
                } catch (Exception ex) {
                    return StatusCode(500, ex.Message);

                }
            }
        }
        [Route(Routes.Routes.DELETE)]
        [HttpPatch]
        public async Task<ActionResult<bool>> UpdateAsync(long ?id) {
            using (LogContext.PushProperty(Constants.CORELLATION_ID, Guid.NewGuid().ToString())) {
                try {
                    if (!id.HasValue ) {
                        return StatusCode(400, "Invalid user provided");
                    }
                    var deleted = await this.userService.DeleteAsync(id.Value);
                    return Ok(deleted);
                } catch (Exception ex) {
                    return StatusCode(500, ex.Message);

                }
            }
        }
    }
}
