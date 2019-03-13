﻿using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CHS.TLC.Data.NM.Web.Helpers
{
    public class ConstantHelpers
    {

        public const Int32 ROL_ADMINISTRADOR_KEY = 1;
        public const String ROL_CAJERO = "CAJ";
        public const String ROL_COMPLETO_CAJERO = "CAJERO";

        public static readonly Int32 DEFAULT_PAGE_SIZE = 25;
        public static readonly Int32 DEFAULT_PAGE_SIZE_MODAL = 10;

        public static class ESTADO
        {
            public const String ACTIVO = "ACT";
            public const String INACTIVO = "INA";

            public static String GetLabelEstado(String Estado)
            {
                switch (Estado)
                {
                    case ACTIVO:
                        return "<span class='badge badge-success'>ACTIVO</span>";
                    case INACTIVO:
                        return "<span class='badge badge-danger'>INACTIVO</span>";
                }
                return "";
            }
            public static String GetNombreEstado(String Estado)
            {
                switch (Estado)
                {
                    case ACTIVO:
                        return "PROCESADO";
                    case INACTIVO:
                        return "INACTIVO";
                }
                return "";
            }
        }
        public static class TIPOBANCO
        {
            public const String NACIONAL = "NAC";
            public const String EXTRANJERO = "EXT";

            public static String GetLabelTipoBanco(String TipoBanco)
            {
                switch (TipoBanco)
                {
                    case NACIONAL:
                        return "<span class='badge badge-warning'>NACIONAL</span>";
                    case EXTRANJERO:
                        return "<span class='badge badge-info'>EXTRANJERO</span>";
                }
                return "";
            }
            public static String GetNombreTipoBanco(String TipoBanco)
            {
                switch (TipoBanco)
                {
                    case NACIONAL:
                        return "NACIONAL";
                    case EXTRANJERO:
                        return "EXTRANJERO";
                }
                return "";
            }
        }
        public static class EXTENSION_REPORTE
        {
            public const String EXCEL = "EXCEL";
            public const String WORD = "WORD";
            public const String PDF = "PDF";
        }
        public static class Layout
        {
            public static readonly String MODAL_LAYOUT_PATH = "~/Views/Shared/_ModalLayout.cshtml";
            public static readonly String MODAL_EMAIL_PATH = "~/Views/Shared/_MailLayout.cshtml";
        }

        public static PagedListRenderOptions Bootstrap3Pager
        {
            get
            {
                return new PagedListRenderOptions
                {
                    DisplayLinkToFirstPage = PagedListDisplayMode.IfNeeded,
                    DisplayLinkToLastPage = PagedListDisplayMode.IfNeeded,
                    DisplayLinkToPreviousPage = PagedListDisplayMode.IfNeeded,
                    DisplayLinkToNextPage = PagedListDisplayMode.IfNeeded,
                    DisplayLinkToIndividualPages = true,
                    DisplayPageCountAndCurrentLocation = false,
                    MaximumPageNumbersToDisplay = 10,
                    DisplayEllipsesWhenNotShowingAllPageNumbers = true,
                    EllipsesFormat = "&#8230;",
                    LinkToFirstPageFormat = "««",
                    LinkToPreviousPageFormat = "«",
                    LinkToIndividualPageFormat = "{0}",
                    LinkToNextPageFormat = "»",
                    LinkToLastPageFormat = "»»",
                    PageCountAndCurrentLocationFormat = "Page {0} of {1}.",
                    ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.",
                    FunctionToDisplayEachPageNumber = null,
                    ClassToApplyToFirstListItemInPager = null,
                    ClassToApplyToLastListItemInPager = null,
                    ContainerDivClasses = new[] { "pagination-container" },
                    UlElementClasses = new[] { "pagination" },
                    LiElementClasses = new[] { "page-item" },
                    FunctionToTransformEachPageLink = (liTag, aTag) => { aTag.Attributes.Add("class", "page-link"); liTag.InnerHtml = aTag.ToString(); return liTag; },
                };
            }
        }
    }
}
