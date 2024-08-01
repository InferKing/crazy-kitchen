using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderPage : SitePage
{
    public override void UpdatePage(SiteSession session)
    {
        ServiceLocator.Instance.Get<EventBus>().Invoke(new CartUpdatedSignal(session));
        // Тут как бы должна подгружаться информация из сессии и отображаться на странице
    }
}
