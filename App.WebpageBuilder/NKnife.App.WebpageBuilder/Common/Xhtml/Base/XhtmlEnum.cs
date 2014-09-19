namespace Jeelu
{
    public sealed class Xhtml
    {
        private Xhtml()
        {

        }
        public enum Version
        {
            /// <summary>
            /// Xhtml1.0严格版本
            /// </summary>
            Xhtml10Strict,
            /// <summary>
            /// Xhtml1.0兼容版本
            /// </summary>
            Xhtml10Transitional,
            /// <summary>
            /// Xhtml1.1版本
            /// </summary>
            Xhtml11,
            /// <summary>
            /// 嘛都不是 :-)
            /// </summary>
            None,
        }

        #region 属性

        public enum Target
        {
            _top, _parent, _self, _blank,
        }

        public enum Charset
        {
            _iso_8859_1, utf_8, shift_j_is, euc_jp, big5, gb2312, euc_kr, din_66003_kr, ns_4551_1_kr, sen_850200_b, cs_iso2022jp, hz_gb_2312, ibm852, ibm866, irv, _iso_2022_kr, _iso_8859_2, _iso_8859_3, _iso_8859_4, _iso_8859_5, _iso_8859_6, _iso_8859_7, _iso_8859_8, _iso_8859_9, koi8_r, ks_c_5601, windows_1250, windows_1251, windows_1252, windows_1253, windows_1254, windows_1255, windows_1256, windows_1257, windows_1258, windows_874, x_euc, asmo_708, dos_720, dos_862, dos_874, cp866, cp1256,
        }

        public enum Dir
        {
            ltr, rtl,
        }

        public enum Lang
        {
            af, sq, ar, eu, br, bg, be, ca, zh, hr, cs, da, nl, en, et, fo, fa, fi, fr, gd, de, el, he, hi, hu, _is, id, it, ja, ko, lv, lt, mk, ms, mt, no, pl, pt, rm, ro, ru, sz, sr, tn, sk, sl, es, sx, sv, th, ts, tr, uk, ur, vi, xh, yi, zu,
        }

        public enum Noexternaldata
        {
            _true, _false,
        }

        public enum Align
        {
            left, right, top, middle, bottom, texttop, absmiddle, baseline, absbottom,
        }

        public enum Size
        {
            _1, _2, _3, _4, _5, _6, _7,
        }

        public enum Loop
        {
            _1,
        }

        public enum Clear
        {
            none, left, right, all,
        }

        public enum Type
        {
            submit, reset, button,
        }

        public enum Valign
        {
            top, bottom,
        }

        public enum Hidden
        {
            _true, _false,
        }

        public enum Autostart
        {
            _false, _true,
        }

        public enum Method
        {
            get, post,
        }

        public enum Enctype
        {
            application_x_www_form_urlencoded, multipart_form_data, text_plain,
        }

        public enum Runat
        {
            server,
        }

        public enum Frameborder
        {
            _1, _0, yes, no,
        }

        public enum Scrolling
        {
            auto, yes, no,
        }

        public enum Visibility
        {
            inherit, show, hide,
        }

        public enum Start
        {
            fileopen, mouseover,
        }

        public enum Accept
        {
            text_html, text_plain, application_msword, application_msexcel, application_postscript, application_x_zip_compressed, application_pdf, application_rtf, video_x_msvideo, video_quicktime, video_x_mpeg2, audio_x_pn_realaudio, audio_x_mpeg, audio_x_waw, audio_x_aiff, audio_basic, image_tiff, image_jpeg, image_gif, image_x_png, image_x_photo_cd, image_x_ms_bmp, image_x_rgb, image_x_portable_pixmap, image_x_portable_greymap, image_x_portablebitmap,
        }

        public enum Media
        {
            screen, tty, tv, projection, handheld, print, braille, aural, all,
        }

        public enum Behavior
        {
            scroll, slide, alternate,
        }

        public enum Direction
        {
            left, right, up, down,
        }

        public enum Http_equiv
        {
            content_type, expires, description, keywords, pics_label, _refresh, reply_to,
        }

        public enum Valuetype
        {
            data, _ref, _object,
        }

        public enum Language
        {
            javascript, javascript1_1, javascript1_2, javascript1_3, javascript1_4, javascript1_5, jscript, vbscript, php, c_sharp, vb,
        }

        public enum Frame
        {
            _void, above, below, hsides, vsides, lhs, rhs, box, border,
        }

        public enum Rules
        {
            none, groups, rows, cols, all,
        }

        public enum Wrap
        {
            off, soft, hard, _virtual, physical,
        }

        #endregion
    }
}

