//using CommunityToolkit.Maui.Animations;
//using Kurama.Controls;

//namespace Kurama.Animations;

//public class VoiceAnimation : BaseAnimation<Voice>
//{
//    Animation Voice(Voice view)
//    {
//        var animation = new Animation();

//        animation.WithConcurrent(f => view.TranslationX = f, view.TranslationX - 500, view.TranslationX + 1200, Easing.Linear);
//        animation.WithConcurrent(f => view.TranslationY = f, view.TranslationY, view.TranslationY + 300, Easing.Linear);
//        animation.WithConcurrent(f => view.Scale = f, 1, 1.5, Easing.Linear);
//        return animation;
//    }

//    public override Task Animate(Voice view, CancellationToken cancellationToken = default)
//    {
//        view.Animate("Voice", Voice(view), 16, Length, repeat: () => true);
//        return Task.CompletedTask;
//    }
//}