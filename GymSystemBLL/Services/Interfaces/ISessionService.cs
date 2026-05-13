using GymSystemBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Services.Interfaces
{
	public interface ISessionService
	{
		IEnumerable<SessionViewModel> GetAll();

		SessionDetailsViewModel? GetById(int id);

		bool CreateSession(CreateSessionViewModel createdSession);

		UpdateSessionViewModel? GetSessionToUpdate(int id);

		bool UpdateSession(int id, UpdateSessionViewModel updateSession);
		bool DeleteSession(int id);
	}
}
