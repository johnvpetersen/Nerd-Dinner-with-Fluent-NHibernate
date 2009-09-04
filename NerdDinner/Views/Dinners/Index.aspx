<%@ Page Inherits="System.Web.Mvc.ViewPage<NerdDinner.Helpers.PaginatedList<NerdDinner.Entities.Dinners>>" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
	Upcoming Dinners
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>
        Upcoming Dinners
    </h2>

    <ul>
    
        <% foreach (var dinner in Model) { %>
        
            <li>     
                <%= Html.ActionLink(dinner.Title, "Details", new { id=dinner.DinnerID }) %>
                on 
                <%= Html.Encode(dinner.EventDate.ToShortDateString())%> 
                @
                <%= Html.Encode(dinner.EventDate.ToShortTimeString())%>
            </li>
        
        <% } %>

    </ul>

    <div class="pagination">

        <% if (Model.HasPreviousPage) { %>
        
            <%= Html.RouteLink("<<<", 
                               "UpcomingDinners", 
                               new { page=(Model.PageIndex-1) }) %>
        
        <% } %>
        
        <% if (Model.HasNextPage) { %>
        
            <%= Html.RouteLink(">>>", 
                               "UpcomingDinners", 
                               new { page = (Model.PageIndex + 1) })%>
        
        <% } %>    

    </div>
    
</asp:Content>


