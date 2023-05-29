// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace PeopleApp.Blazor.Manage
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
#nullable restore
#line 1 "C:\Users\20003843\Desktop\CS_Web_2\NET6\4_0-People-BlazorWebAssembly-Start\PeopleApp.Server\_Imports.razor"
using Microsoft.AspNetCore.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\20003843\Desktop\CS_Web_2\NET6\4_0-People-BlazorWebAssembly-Start\PeopleApp.Server\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\20003843\Desktop\CS_Web_2\NET6\4_0-People-BlazorWebAssembly-Start\PeopleApp.Server\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\20003843\Desktop\CS_Web_2\NET6\4_0-People-BlazorWebAssembly-Start\PeopleApp.Server\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\20003843\Desktop\CS_Web_2\NET6\4_0-People-BlazorWebAssembly-Start\PeopleApp.Server\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\20003843\Desktop\CS_Web_2\NET6\4_0-People-BlazorWebAssembly-Start\PeopleApp.Server\_Imports.razor"
using PeopleApp.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\20003843\Desktop\CS_Web_2\NET6\4_0-People-BlazorWebAssembly-Start\PeopleApp.Server\_Imports.razor"
using PeopleApp.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\20003843\Desktop\CS_Web_2\NET6\4_0-People-BlazorWebAssembly-Start\PeopleApp.Server\_Imports.razor"
using PeopleApp.Shared.Api.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\20003843\Desktop\CS_Web_2\NET6\4_0-People-BlazorWebAssembly-Start\PeopleApp.Server\_Imports.razor"
using PeopleApp.Shared.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\20003843\Desktop\CS_Web_2\NET6\4_0-People-BlazorWebAssembly-Start\PeopleApp.Server\Blazor\Manage\EditPerson.razor"
using Microsoft.Extensions.DependencyInjection;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Components.RouteAttribute("/manage/people/edit/{id:long}")]
    public partial class EditPerson : OwningComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 34 "C:\Users\20003843\Desktop\CS_Web_2\NET6\4_0-People-BlazorWebAssembly-Start\PeopleApp.Server\Blazor\Manage\EditPerson.razor"
       
    public IPersonRepository PersonRepository => ScopedServices.GetService<IPersonRepository>();

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Parameter]
    public long Id { get; set; }

    public Person Person { get; set; }

    protected override void OnParametersSet()
    {
        Person = PersonRepository.GetById(Id);
    }

    public void HandleValidSubmit()
    {
        PersonRepository.Update(Person);
        NavigationManager.NavigateTo("/manage/people");
    }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591