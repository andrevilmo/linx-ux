

   ----------------------------------------------------------------------
             README file for Web Markup Minifier: MS Ajax 0.9.11

   ----------------------------------------------------------------------

      Copyright (c) 2013-2015 Andrey Taritsyn - http://www.taritsyn.ru
		  
		  
   ===========
   DESCRIPTION
   ===========   
   WebMarkupMin.MsAjax contains 2 minifier-adapters: 
   MsAjaxCssMinifier (for minification of CSS code) and 
   MsAjaxJsMinifier (for minification of JS code). These adapters 
   perform minification using the Microsoft Ajax Minifier 
   (http://ajaxmin.codeplex.com).
   
   =============
   RELEASE NOTES
   =============
   1. Added support of the Microsoft Ajax Minifier version 5.14;
   2. In the `CssColor` enumeration added new value - `NoSwap`;
   3. In CSS minification settings changed the default value for
      `ColorNames` property (instead of `Strict` now is used `Hex`).
   
   ====================
   POST-INSTALL ACTIONS
   ====================
   To make MsAjaxCssMinifier is the default CSS minifier and 
   MsAjaxJsMinifier is the default JS minifier, you need to make
   changes to the Web.config file. 
   In defaultMinifier attribute of element 
   \configuration\webMarkupMin\core\css must be set value equal to 
   MsAjaxCssMinifier, and in same attribute of element 
   \configuration\webMarkupMin\core\js - MsAjaxJsMinifier.

   =============
   DOCUMENTATION
   =============
   See more detailed information on CodePlex -
   http://webmarkupmin.codeplex.com