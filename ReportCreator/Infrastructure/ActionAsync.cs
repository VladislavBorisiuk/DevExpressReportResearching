﻿using System.Threading.Tasks;

namespace ReportCreator.Infrastructure
{
    internal delegate Task ActionAsync();

    internal delegate Task ActionAsync<in T>(T parameter);
}
