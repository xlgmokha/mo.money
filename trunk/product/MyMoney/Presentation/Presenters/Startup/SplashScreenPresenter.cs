using MoMoney.Infrastructure.Extensions;
using MoMoney.Infrastructure.Threading;
using MoMoney.Presentation.Views.Startup;
using MoMoney.Utility.Core;

namespace MoMoney.Presentation.Presenters.Startup
{
    public interface ISplashScreenPresenter : IDisposableCommand, ITimerClient
    {
    }

    public class SplashScreenPresenter : ISplashScreenPresenter
    {
        readonly ITimer timer;
        readonly ISplashScreenView view;
        ISplashScreenState current_state;

        public SplashScreenPresenter(ITimer timer, ISplashScreenView view)
        {
            this.timer = timer;
            this.view = view;
        }

        public void run()
        {
            view.display();
            current_state = new display_the_splash_screen(timer, view, this);
        }

        public void Dispose()
        {
            current_state = new hide_the_splash_screen(timer, view, this);
        }

        public void notify()
        {
            this.log().debug("update the presenter");
            current_state.update();
        }
    }
}