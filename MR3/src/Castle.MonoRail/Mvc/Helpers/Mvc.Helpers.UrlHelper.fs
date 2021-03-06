﻿//  Copyright 2004-2011 Castle Project - http://www.castleproject.org/
//  Hamilton Verissimo de Oliveira and individual contributors as indicated. 
//  See the committers.txt/contributors.txt in the distribution for a 
//  full listing of individual contributors.
// 
//  This is free software; you can redistribute it and/or modify it
//  under the terms of the GNU Lesser General Public License as
//  published by the Free Software Foundation; either version 3 of
//  the License, or (at your option) any later version.
// 
//  You should have received a copy of the GNU Lesser General Public
//  License along with this software; if not, write to the Free
//  Software Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA
//  02110-1301 USA, or see the FSF site: http://www.fsf.org.

namespace Castle.MonoRail.Helpers

    open System
    open System.Collections.Generic
    open System.Text
    open System.Linq
    open System.Linq.Expressions
    open System.Web
    open Castle.MonoRail
    open Castle.MonoRail.ViewEngines


    type public UrlHelper(ctx) = 
        inherit BaseHelper(ctx)
        let mutable _appPath = ctx.HttpContext.Request.ApplicationPath

        member x.AppPath
            with get() = _appPath and set v = _appPath <- v

        static member Combine( url1:string, url2:string ) = 
            url1.TrimEnd([|'/'|]) + "/" + url2.TrimStart([|'/'|])

        member x.Content(url:string) : IHtmlStringEx = 
            // <app_path>/Content/<url>
            upcast HtmlResult (UrlHelper.Combine (UrlHelper.Combine(_appPath, "Content"), url))

        member x.For(url:TargetUrl) : IHtmlStringEx = 
            upcast HtmlResult(url.Generate null)

        member x.For(url:TargetUrl, urlParams:IDictionary<string,string>) : IHtmlStringEx = 
            upcast HtmlResult(url.Generate urlParams)

        member x.Link(url:TargetUrl, text:string) : IHtmlStringEx = 
            x.InternalLink(url, text, null, null)

        member x.Link(url:TargetUrl, text:string, html:IDictionary<string,string>) : IHtmlStringEx = 
            x.InternalLink(url, text, html, null)

        member x.Link(url:TargetUrl, text:string, html:IDictionary<string,string>, urlParams:IDictionary<string,string>) : IHtmlStringEx = 
            x.InternalLink(url, text, html, urlParams)

        member internal x.InternalLink(url:TargetUrl, text:string, html:IDictionary<string,string>, urlParams:IDictionary<string,string>) : IHtmlStringEx = 
            upcast HtmlResult(sprintf "<a href=\"%s\"%s>%s</a>" (url.Generate urlParams) (x.AttributesToString html) (text |> x.HtmlEncode))
