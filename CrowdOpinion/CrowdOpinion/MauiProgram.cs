﻿using Microsoft.Extensions.Logging;
using UraniumUI;
using CrowdOpinion.ViewModels;
using CrowdOpinion.Models;
using CrowdOpinion.Pages;
using CrowdOpinion.Services;
using Supabase;

namespace CrowdOpinion
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddMaterialIconFonts();
                });

            // Configure Supabase
            var url = AppConfig.SUPABASE_URL;
            var key = AppConfig.SUPABASE_KEY;

            builder.Services.AddSingleton(provider => new Supabase.Client(url, key));

            builder.Services.AddSingleton<QuestionStore>();
            builder.Services.AddSingleton<AskViewModel>();
            builder.Services.AddSingleton<Ask>();
            builder.Services.AddSingleton<AnswerViewModel>();
            builder.Services.AddSingleton<Answer>();
            builder.Services.AddSingleton<ProfileViewModel>();
            builder.Services.AddSingleton<Profile>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<Login>();
            builder.Services.AddSingleton<SettingsViewModel>();
            builder.Services.AddSingleton<Settings>();

            // Add Data Service
            builder.Services.AddSingleton<IDataService, DataService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
