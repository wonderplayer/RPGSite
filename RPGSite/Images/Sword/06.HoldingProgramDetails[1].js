/// <reference path="MyHoldingsBase.js" />
/// <reference path="jquery-1.3.1-vsdoc.js" />
/// <reference path="MyHoldingsMain.js" />

// Holdings Details Container
MyHoldings.Container.HoldingProgramDetails = function (containerid) {
    this._containerid = containerid;
    this._programtypeid = undefined;
    this._overrideprice = null;
    this._isoverrideprice = 0;
    this._isoverrideexchrate = 0;
    this._exchrate = null;
    this._holdingnum = null;
    this._holdingtype = null;
    this._accordionflag = null;
    this._isinitialized = false;
    this._iscompressed = false;
    this._grantlist = "";
    this._defaultstoagreement = false;
    this._mainpanelflag = null;
    this._fromalertrequest = false;
    this._initialload = null;
    this._ishide = null;
    this._overridecurrencycd = null;
};

MyHoldings.Container.HoldingProgramDetails.prototype = new MyHoldings.Container;
MyHoldings.Container.HoldingProgramDetails.prototype.get_programtypeid = function () { return this._programtypeid; }
MyHoldings.Container.HoldingProgramDetails.prototype.set_programtypeid = function (value) { this._programtypeid = value; }
MyHoldings.Container.HoldingProgramDetails.prototype.get_overrideprice = function () { return this._overrideprice; }
MyHoldings.Container.HoldingProgramDetails.prototype.set_overrideprice = function (value) { this._overrideprice = value; }
MyHoldings.Container.HoldingProgramDetails.prototype.get_isoverrideprice = function () { return this._isoverrideprice; }
MyHoldings.Container.HoldingProgramDetails.prototype.set_isoverrideprice = function (value) { this._isoverrideprice = value; }
MyHoldings.Container.HoldingProgramDetails.prototype.get_isoverrideexchrate = function () { return this._isoverrideexchrate; }
MyHoldings.Container.HoldingProgramDetails.prototype.set_isoverrideexchrate = function (value) { this._isoverrideexchrate = value; }
MyHoldings.Container.HoldingProgramDetails.prototype.get_exchrate = function () { return this._exchrate; }
MyHoldings.Container.HoldingProgramDetails.prototype.set_exchrate = function (value) { this._exchrate = value; }

MyHoldings.Container.HoldingProgramDetails.prototype.get_overridecurrencycd = function () { return this._overridecurrencycd; }
MyHoldings.Container.HoldingProgramDetails.prototype.set_overridecurrencycd = function (value) { this._overridecurrencycd = value; }

MyHoldings.Container.HoldingProgramDetails.prototype.get_ishide = function () { return this._ishide; }
MyHoldings.Container.HoldingProgramDetails.prototype.set_ishide = function (value) { this._ishide = value; }


MyHoldings.Container.HoldingProgramDetails.prototype.get_holdingnum = function () { return this._holdingnum; }
MyHoldings.Container.HoldingProgramDetails.prototype.set_holdingnum = function (value) { this._holdingnum = value }
MyHoldings.Container.HoldingProgramDetails.prototype.get_holdingtype = function () { return this._holdingtype; }
MyHoldings.Container.HoldingProgramDetails.prototype.set_holdingtype = function (value) { this._holdingtype = value }
MyHoldings.Container.HoldingProgramDetails.prototype.get_accordionflag = function () { return this._accordionflag; }
MyHoldings.Container.HoldingProgramDetails.prototype.set_accordionflag = function (value) { this._accordionflag = value }

MyHoldings.Container.HoldingProgramDetails.prototype.ChangeSizeGetValue = function () {
    if ($("#forwardback-btn").hasClass('forward-btn') == true) {
        return false
    } else return true;
}


MyHoldings.Container.HoldingProgramDetails.prototype.ChangeSize = function (tocompress) {
    var self = this;

    if (tocompress) {
        $("#forwardback-btn").removeClass('back-btn');
        $("#forwardback-btn").addClass('forward-btn');
        $("#forwardback-btn").empty();
        $("#forwardback-btn").append(' <img src="../../Images/ExpandButton_small.png" class="expand" alt="Expand button">');
        $(".details-grid-header").css('width', '402px');
        $(".details-grid-header").css('float', 'left');
        $(".program-details-contents").css('width', '403px');
        $(".details-grid-group").css('width', '401px');
        $(".details-grid-cont").css('width', '399px');
        $(".details-grid-footer").css('width', '399px');
        $(".details-table-header").css('width', '385px');
        $(".imgButtonDiv").css('width', '401px');
        $("*.detailhidable").addClass('invisible');
        $("*.detailhidable a").addClass('invisible');
        $("#gvProgramDetails > tbody").css('width', '401px');
        $(".details-table-header > thead").css('width', '401px');

        self._iscompressed = true;
    }
    else {

        $("#forwardback-btn").removeClass('forward-btn');
        $("#forwardback-btn").addClass('back-btn');
        $("#forwardback-btn").empty();
        $("#forwardback-btn").append(' <img src="../../Images/CollapseButton_small.PNG" class="collapse" alt="Collapse button">');
        $(".details-grid-header").css('width', '970px');
        $(".program-details-contents").css('width', '972px');
        $(".details-grid-group").css('width', '970px');
        $(".details-grid-cont").css('width', '969px');
        $(".details-grid-footer").css('width', '969px');
        $(".details-table-header").css('width', '960px');
        $(".imgButtonDiv").css('width', '970px');
        $("#gvProgramDetails > tbody").css('width', '970px');

        $("*.detailhidable").removeClass('invisible');
        $(".hideheader").addClass('invisible');
        $("*.detailhidable a").removeClass('invisible');
        $(".details-table-header > thead").css('width', '970px');

        self._iscompressed = false;
    }

    var expanddetails = MyHoldings.Container.HoldingProgramDetails.prototype.ChangeSizeGetValue();
    var expandsummary = MyHoldings.Container.HoldingProgramSummary.prototype.ChangeSizeGetValue();

    if (expanddetails == true && expandsummary == true) {
        $("[id$='summary-details-bridge']").addClass("addbridge");
    }
    else {
        $("[id$='summary-details-bridge']").removeClass("addbridge");
    }

    MyHoldings.Footnotes.RefreshFootnotes();
}


MyHoldings.Container.HoldingProgramDetails.prototype.init = function () {

    this._isinitialized = true;
    var self = this;
    var count = 0;

    self.raise_event("load_complete", {});

    MyHoldings.Container.HoldingProgramDetails.prototype.SortMe("", "1");

    var browserInfo = getBrowserInfo();

    if (browserInfo.indexOf("Chrome") != -1) { //If chrome
        $('.imageCollapse2').keydown(function (e) {
            var src = $('.prgDetailsImg').children("img").attr('src');
            var origElement = null;
            if (e.keyCode == 13) {

                if ($('#forwardback-btn').hasClass("back-btn") && src == "../../Images/CollapseButton_small.PNG") {
                    $("[id$='summary-details-bridge']").removeClass("addbridge");
                    $("#forwardback-btn").removeClass('back-btn');
                    $("#forwardback-btn").addClass('forward-btn');
                    $("#forwardback-btn").empty();
                    $("#forwardback-btn").append(' <img src="../../Images/ExpandButton_small.png" style="border: 2px dotted #2F539D;" class="expand" title="Expand button"> <span class="hiddentext state">Program Details Table is collapsed, activate expand button to expand table</span>');
                    $(".details-grid-header").css('width', '402px');
                    $(".program-details-contents").css('width', '403px');
                    $(".details-grid-group").css('width', '401px');
                    $(".details-grid-cont").css('width', '399px');
                    $(".details-grid-footer").css('width', '399px');
                    $(".details-table-header").css('width', '385px');
                    $(".imgButtonDiv").css('width', '401px');
                    $("*.detailhidable").addClass('invisible');
                    $("*.detailhidable a").addClass('invisible');
                    $("#gvProgramDetails > tbody").css('width', '401px');
                    $(".details-table-header > thead").css('width', '401px');

                    origElement = document.activeElement;
                    $(".state").focus();

                    // alert("Program Details Table is Collapsed");

                }
                else if ($('#forwardback-btn').hasClass("forward-btn") && src == "../../Images/ExpandButton_small.png") {

                    $("#forwardback-btn").removeClass('forward-btn');
                    $("#forwardback-btn").addClass('back-btn');
                    $("#forwardback-btn").empty();
                    $("#forwardback-btn").append(' <img src="../../Images/CollapseButton_small.PNG" style="border: 2px dotted #2F539D;" class="collapse" title="Collapse button"> <span class="hiddentext state">Program Details Table is expanded, activate collapse button to collapse table</span>');
                    $(".details-grid-header").css('width', '972px');
                    $(".program-details-contents").css('width', '972px');
                    $(".details-grid-group").css('width', '970px');
                    $(".details-grid-cont").css('width', '969px');
                    $(".details-grid-footer").css('width', '969px');
                    $(".details-table-header").css('width', '960px');
                    $(".imgButtonDiv").css('width', '970px');
                    $("#gvProgramDetails > tbody").css('width', '970px');
                    $("*.posrelative").removeClass('invisible');
                    $(".hideheader").addClass('invisible');
                    $(".detailhidable.invisible").removeClass('invisible');
                    $(".hideheader.detailhidable").addClass('invisible');
                    $(".details-table-header tr th div > a").removeClass("invisible");
                    $(".details-table-header > thead").css('width', '970px');

                    origElement = document.activeElement;
                    $(".state").focus();

                    // alert("Program Details Table is Expanded");
                }

                origElement.focus();
            }

        });
    } else {
        $('.imageCollapse2').keypress(function (e) {
            var src2 = $('.prgDetailsImg').children("img").attr('src');
            var origElement = null;

            if (e.keyCode == 13) {
                if ($('#forwardback-btn').hasClass("back-btn") && src2 == "../../Images/CollapseButton_small.PNG") {
                    $("[id$='summary-details-bridge']").removeClass("addbridge");
                    $("#forwardback-btn").removeClass('back-btn');
                    $("#forwardback-btn").addClass('forward-btn');
                    $("#forwardback-btn").empty();
                    $("#forwardback-btn").append(' <img src="../../Images/ExpandButton_small.png" style="border: 2px dotted #2F539D;" class="expand" title="Expand button"><span class="hiddentext state">Program Details Table is collapsed, activate expand button to expand table</span> ');
                    $(".details-grid-header").css('width', '403px');
                    $(".program-details-contents").css('width', '403px');
                    $(".details-grid-group").css('width', '401px');
                    $(".details-grid-cont").css('width', '399px');
                    $(".details-grid-footer").css('width', '399px');
                    $(".details-table-header").css('width', '385px');
                    $(".imgButtonDiv").css('width', '401px');
                    $("*.detailhidable").addClass('invisible');
                    $("*.detailhidable a").addClass('invisible');
                    $("#gvProgramDetails > tbody").css('width', '401px');
                    $(".details-table-header > thead").css('width', '401px');

                    origElement = document.activeElement;

                    $(".state").focus();
                    MyHoldings.Container.HoldingProgramSummary.Focus("FocusOut");

                }
                else if ($('#forwardback-btn').hasClass("forward-btn") && src2 == "../../Images/ExpandButton_small.png") {

                    $("#forwardback-btn").removeClass('forward-btn');
                    $("#forwardback-btn").addClass('back-btn');
                    $("#forwardback-btn").empty();
                    $("#forwardback-btn").append(' <img src="../../Images/CollapseButton_small.PNG" style="border: 2px dotted #2F539D;" class="collapse" title="Collapse button"> <span class="hiddentext state">Program Details Table is expanded, activate collapse button to collapse table</span>');
                    $(".details-grid-header").css('width', '972px');
                    $(".program-details-contents").css('width', '972px');
                    $(".details-grid-group").css('width', '970px');
                    $(".details-grid-cont").css('width', '969px');
                    $(".details-grid-footer").css('width', '969px');
                    $(".details-table-header").css('width', '960px');
                    $(".imgButtonDiv").css('width', '970px');
                    $("#gvProgramDetails > tbody").css('width', '970px');
                    $("*.posrelative").removeClass('invisible');
                    $(".hideheader").addClass('invisible');
                    $(".detailhidable.invisible").removeClass('invisible');
                    $(".hideheader.detailhidable").addClass('invisible');
                    $(".details-table-header tr th div > a").removeClass("invisible");
                    $(".details-table-header > thead").css('width', '970px');
                    
                    origElement = document.activeElement;

                    $(".state").focus();
                    MyHoldings.Container.HoldingProgramSummary.Focus("FocusOut");
                }
                origElement.focus();
            }
        });
    }

    //TOTAL VEIP AND VEIP MATCHING TOOLTIP
    $("#gvProgramDetails tbody tr").each(function (i) {

        //HoldingsType
        var type = $("#gvProgramDetails tbody tr").children("td:nth-child(13)").children(":input").attr("value");
        if (type == 6) {
            count = count + 1;
        }
    });
    $("[id$='detailsTotalRemaining']").hover(function () {
        if (count > 0) {
            $("#divVeipTotalRemainingTooltip").show();
        }
    }, function () {
        $("#divVeipTotalRemainingTooltip").hide();
    });

    $("[id$='detailsTotalShareValue']").hover(function () {
        if (count > 0) {
            $("#divVeipTotalValueTooltip").show();
        }
    }, function () {
        $("#divVeipTotalValueTooltip").hide();
    });

    MyHoldings.Utilities.CreateTooltip();

    //Highlight on Hover
    $('#' + self._containerid + " tbody tr.holdings-details-item").hover(function () {
        $(this).addClass('pretty-hover2');
    }, function () {
        $(this).removeClass('pretty-hover2');
    });

    $(".holdings-details tbody tr.holdings-details-item:nth(0)").click();

    //Broker Pop-up Link
    $("a.brokerpopup").click(function (event) {
        event.stopPropagation();
        $(".brokerContainer").html(''); //KDA, 7/29/2009, CIO00169633

        var a = $(this).attr("brokerlist").split('/');
        var counter = 1;

        document.getElementById('brokertemp').innerHTML = "";
        document.getElementById('brokerdetailcontainer').innerHTML = "";

        //SGB:8/04/2009, CIO00169633
        $("#brokerdetailcontainer").html("<div id='load' style='text-align:center; vertical-align:middle'><img src='" + MyHoldings.Resources["LoadingImageUrl"] + "' alt='Loading Control' /></div>");

        var line;
        for (var i = 0; i < a.length; i++) {

            //BEGIN: KDA, 7.1.2009,  CIO00159419
            if (i < a.length - 1) {
                line = "</br><hr>";
            }
            //END

            MyHoldings.Utilities.LoadPage("brokertemp", MyHoldings.Resources["HoldingsDetail" + a[i]], {}, function () {

                if (counter < a.length) {
                    line = "</br><hr>";
                }

                $(".brokerContainer").append(document.getElementById('brokertemp').innerHTML);
                $(".brokerContainer").append(line); //KDA, 7.1.2009,  CIO00159419
                line = ""; //KDA, 7.1.2009,  CIO00159419

                if (counter == a.length) {
                    $("#load").remove();
                }
                counter = counter + 1;
            }, 0);
        }

        MyHoldings.Utilities.CreateDialogBoxBrokerDetail("brokerContainer");
    });

    //Grant Status Link
    $(".grantlink").click(function (event) {
        self._defaultstoagreement = true;
    });

    //On Details Item Click
    $('#' + self._containerid + " tbody tr.holdings-details-item," + '#' + self._containerid + " tr.selected2").click(function () {
        $('#' + self._containerid + " tbody tr.selected2").removeClass("selected2");

        // retrieve selected HoldingNum
        var holdingnum = $(this).addClass("selected2").children("td:nth-child(12)").children(":input").attr("value");
        self._holdingnum = holdingnum;
        $(".program-details-contents").css('margin-top', '28px');

        // retrieve selected HoldingType
        var holdingtype = $(this).addClass("selected2").children("td:nth-child(13)").children(":input").attr("value");
        self._holdingtype = holdingtype;

        // retrieve the accordion flag
        var accordionflag = $(this).addClass("selected2").children("td:nth-child(22)").children(":input").attr("value");
        self._accordionflag = accordionflag;

        //Make sure that the control is resized to a small container
        self.ChangeSize(true);
        // raise event
        self.raise_event("item_clicked", { holdingtype: self._holdingtype, holdingnum: self._holdingnum, defaultstoagreement: self._defaultstoagreement, accordionflag: self._accordionflag });
        if (self._fromalertrequest) {
            self._defaultstoagreement = true;
        } else {
            self._defaultstoagreement = false;
        }

    });

    //PWD Keyboard accessibility table

    function getBrowserInfo() {
        var ua = navigator.userAgent, tem,
	M = ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || [];
        if (/trident/i.test(M[1])) {
            tem = /\brv[ :]+(\d+)/g.exec(ua) || [];
            return 'IE ' + (tem[1] || '');
        }
        if (M[1] === 'Chrome') {
            tem = ua.match(/\b(OPR|Edge)\/(\d+)/);
            if (tem != null) return tem.slice(1).join(' ').replace('OPR', 'Opera');
        }
        M = M[2] ? [M[1], M[2]] : [navigator.appName, navigator.appVersion, '-?'];
        if ((tem = ua.match(/version\/(\d+)/i)) != null)
            M.splice(1, 1, tem[1]);
        return M.join(' ');
    }

    var browserInfo = getBrowserInfo();
    if (browserInfo.indexOf("Chrome") != -1) {
        $('#' + self._containerid + " tbody tr.holdings-details-item," + '#' + self._containerid + " tr.selected2").keydown(function (e) {
            if (e.keyCode == 13) {
                $('#' + self._containerid + " tbody tr.selected2").removeClass("selected2");

                // retrieve selected HoldingNum
                var holdingnum = $(this).addClass("selected2").children("td:nth-child(12)").children(":input").attr("value");
                self._holdingnum = holdingnum;

                // retrieve selected HoldingType
                var holdingtype = $(this).addClass("selected2").children("td:nth-child(13)").children(":input").attr("value");
                self._holdingtype = holdingtype;

                // retrieve the accordion flag
                var accordionflag = $(this).addClass("selected2").children("td:nth-child(22)").children(":input").attr("value");
                self._accordionflag = accordionflag;

                //Make sure that the control is resized to a small container
                self.ChangeSize(true);
                // raise event
                self.raise_event("item_clicked", { holdingtype: self._holdingtype, holdingnum: self._holdingnum, defaultstoagreement: self._defaultstoagreement, accordionflag: self._accordionflag });
                if (self._fromalertrequest) {
                    self._defaultstoagreement = true;
                } else {
                    self._defaultstoagreement = false;
                }
            }
        });
    } else {
        $('#' + self._containerid + " tbody tr.holdings-details-item," + '#' + self._containerid + " tr.selected2").keypress(function (e) {
            if (e.keyCode == 13) {
                $('#' + self._containerid + " tbody tr.selected2").removeClass("selected2");

                // retrieve selected HoldingNum
                var holdingnum = $(this).addClass("selected2").children("td:nth-child(12)").children(":input").attr("value");
                self._holdingnum = holdingnum;

                // retrieve selected HoldingType
                var holdingtype = $(this).addClass("selected2").children("td:nth-child(13)").children(":input").attr("value");
                self._holdingtype = holdingtype;

                // retrieve the accordion flag
                var accordionflag = $(this).addClass("selected2").children("td:nth-child(22)").children(":input").attr("value");
                self._accordionflag = accordionflag;

                //Make sure that the control is resized to a small container
                self.ChangeSize(true);
                // raise event
                self.raise_event("item_clicked", { holdingtype: self._holdingtype, holdingnum: self._holdingnum, defaultstoagreement: self._defaultstoagreement, accordionflag: self._accordionflag });
                if (self._fromalertrequest) {
                    self._defaultstoagreement = true;
                } else {
                    self._defaultstoagreement = false;
                }
            }
        });
    }

    $("#forwardback-btn").click(function () {
        self.ChangeSize(!self._iscompressed);
    });

    MyHoldings.Footnotes.RefreshFootnotes();

    if (self._mainpanelflag != 0) {
        self.ChangeSize(true);
        MyHoldings.Container.HoldingProgramSummary.prototype.ChangeSize(true);
    }

    var mainpanel = $(".top-question-alert-container").html();
    if (self._defaultstoagreement == false && self._initialload == true && self._programtypeid != undefined) {

        if (mainpanel == null) {
            var holdingnum = $("#gvProgramDetails tbody tr:first").addClass("selected2").children("td:nth-child(12)").children(":input").attr("value");
            self._holdingnum = holdingnum;
            var holdingtype = $("#gvProgramDetails tbody tr:first").addClass("selected2").children("td:nth-child(13)").children(":input").attr("value");
            self._holdingtype = holdingtype;
            self._defaultstoagreement = false;

            var accordionflag = $("#gvProgramDetails tbody tr:first").addClass("selected2").children("td:nth-child(22)").children(":input").attr("value");
            self._accordionflag = accordionflag;

            //Check if there are records in details section
            var checkifnorecords = $("#gvProgramDetails tbody tr:first").html();

            if (checkifnorecords != null) {
                self.raise_event("item_clicked", { holdingtype: self._holdingtype, holdingnum: self._holdingnum, defaultstoagreement: self._defaultstoagreement, accordionflag: self.accordionflag });
                self.ChangeSize(true);
            }

        }
    } else if (self._defaultstoagreement == false && self._programtypeid == undefined) {

        if (mainpanel == null) {
            var holdingnum = $("#gvProgramDetails tbody tr:first").addClass("selected2").children("td:nth-child(12)").children(":input").attr("value");
            self._holdingnum = holdingnum;
            var holdingtype = $("#gvProgramDetails tbody tr:first").addClass("selected2").children("td:nth-child(13)").children(":input").attr("value");
            self._holdingtype = holdingtype;
            self._defaultstoagreement = false;

            var accordionflag = $("#gvProgramDetails tbody tr:first").addClass("selected2").children("td:nth-child(22)").children(":input").attr("value");
            self._accordionflag = accordionflag;

            //Check if there are records in details section
            var checkifnorecords = $("#gvProgramDetails tbody tr:first").html();

            if (checkifnorecords != null) {
                self.raise_event("item_clicked", { holdingtype: self._holdingtype, holdingnum: self._holdingnum, defaultstoagreement: self._defaultstoagreement, accordionflag: self.accordionflag });
                self.ChangeSize(true);
            }

        }

    } else if (self._defaultstoagreement == true) {
        $('#' + self._containerid + " tbody tr.holdings-details-item:has(input[value='" + self._holdingnum + "'])").addClass("selected2");
    }


    var expanddetails = MyHoldings.Container.HoldingProgramDetails.prototype.ChangeSizeGetValue();
    var expandsummary = MyHoldings.Container.HoldingProgramSummary.prototype.ChangeSizeGetValue();

    if (expanddetails == true && expandsummary == true) {
        $("[id$='summary-details-bridge']").addClass("addbridge");
    }
    else {
        $("[id$='summary-details-bridge']").removeClass("addbridge");
    }

    $("#brokerdetailcontainer").remove();
    $("#brokerdetailParentContainer").append("<div id='brokerdetailcontainer' class='brokerContainer' title='Broker Contact'></div>");

    //BEGIN: Highlight Header on hover
    $("#gvProgramDetails tr th").hover(function () {
        $(this).css('cursor', 'pointer');
        $(this).css({ 'background-color': '#7BC2CC' });
        var cssClass = $(this).attr("class").split(' ');
        var counter = 0;

        if ($('.' + cssClass[0] + ' div > a').find('img[class="ascending"]').length > 0) {
            counter = 1;
        } else if ($('.' + cssClass[0] + ' div > a').find('img[class="descending"]').length > 0) {
            counter = 1;
        }

        if (counter != 1) {
            $('.' + cssClass[0] + ' div > a').append(' <img style="position: absolute; top: 7px; right: 0px;" src="../../Images/bg_arrow.gif" class="expand" alt="sortable column, : activate to sort column ascending">');
        }


    }, function () {
        $(this).css({ 'background-color': '#A3D4DB' });
        var cssClass = $(this).attr("class").split(' ');
        $('.' + cssClass[0] + ' div > a > img[class="expand"]').remove();
    })
    //END: Highlight Header on hover

    //Begin: Sorting Part
    $("#gvProgramDetails tr th").click(function () {
        var self = this;
        var colid = $(this).attr("columnid");
        var cnt = 0;

        if ($("#gvProgramDetails tbody tr").html() != null) {
            $(".details-table-header tr th").each(function () {
                if ($(this).attr("columnid") != colid) {
                    $(this).attr("order", "0");
                }
            })

            MyHoldings.Container.HoldingProgramDetails.prototype.SortMe(self, "0");
            $("#gvProgramDetails tbody tr").removeClass("grid-extra-bgcolor");
            $("#gvProgramDetails tbody tr:even").addClass("grid-extra-bgcolor");
        }
    })

    var browserInfo2 = getBrowserInfo();
    if (browserInfo2.indexOf("Chrome") > -1) {
        $("#gvProgramDetails tr th").keypress(function (e) {
            if (e.keyCode == 13) {
                var self = this;
                var colid = $(this).attr("columnid");
                var cnt = 0;
                //$(this).children("a").focus();

                if ($("#gvProgramDetails tbody tr").html() != null) {
                    $(".details-table-header tr th").each(function () {
                        if ($(this).attr("columnid") != colid) {
                            $(this).attr("order", "0");
                        }
                    })
                    MyHoldings.Container.HoldingProgramDetails.prototype.SortMe(self, "0");
                    $("#gvProgramDetails tbody tr").removeClass("grid-extra-bgcolor");
                    $("#gvProgramDetails tbody tr:even").addClass("grid-extra-bgcolor");
                }
            }
        });
    } else {
        $("#gvProgramDetails tr th").keydown(function (e) {
            if (e.keyCode == 13) {
                var self = this;
                var colid = $(this).attr("columnid");
                var cnt = 0;
                //$(this).children("a").focus();

                if ($("#gvProgramDetails tbody tr").html() != null) {
                    $(".details-table-header tr th").each(function () {
                        if ($(this).attr("columnid") != colid) {
                            $(this).attr("order", "0");
                        }
                    })
                    MyHoldings.Container.HoldingProgramDetails.prototype.SortMe(self, "0");
                    $("#gvProgramDetails tbody tr").removeClass("grid-extra-bgcolor");
                    $("#gvProgramDetails tbody tr:even").addClass("grid-extra-bgcolor");
                }
            }
        });
    }



    //Show sorting button on focus
    $("#gvProgramDetails tr th a").focus(function () {
        var cssClass = $(this).html();

        var counter = 0;
        if ($(this).find('img[class="ascending"]').length > 0) {
            counter = 1;
        } else if ($(this).find('img[class="descending"]').length > 0) {
            counter = 1;
        }

        if (counter != 1) {
            $(this).append(' <img style="position: absolute; top: 7px; right: 0px;" src="../../Images/bg_arrow.gif" class="expand" alt="sortable column, : activate to sort column ascending">');
        }
    }).blur(function () {
        var classValue = $(this).html();

        if (classValue.indexOf('class="expand"') > -1) {
            $(this).children("img").remove();
        }

    });


    $(".details-grid-header tr th").css({ 'cursor': 'pointer' });
    $("#gvProgramDetails tbody tr:even").addClass("grid-extra-bgcolor");

    $("#gvProgramDetails").tablesorter({
        textExtraction: function (node) {
            var text = node.innerHTML.replace(/&nbsp;/g, '').replace(/,/g, '');

            if ($(node).find('a').html() != null) {
                return $(node).find('a').html();
            }
            else {
                if (!isNaN(text) && text != null && text != '') {
                    return node.innerHTML.replace(/&nbsp;/g, '').replace(/,/g, '');
                } else {
                    return node.innerHTML.replace(/&nbsp;/g, '');
                }
            }
        }
    });
    //End: Sorting Part

    $(".details-grid-header tr th.hcol1").click();

    MyHoldings.Container.HoldingProgramDetails.prototype.KeyExec(self._exchrate);


}

MyHoldings.Container.HoldingProgramDetails.prototype.load = function () {
    var self = this;
    var compid = this._containerid;
    var data = { COMPONENTID: compid }
    var programid = this._programtypeid;
    var grantlist = this._grantlist;
    var overrideprice = this._overrideprice;
    var isoverrideprice = this._isoverrideprice;
    var isoverrideexchrate = this._isoverrideexchrate;
    var exchrate = this._exchrate;
    var ishide = this._ishide;
    var overridecurrencycd = this._overridecurrencycd;
    xchng = exchrate;
    if (programid) {
        data.programid = programid;
    }

    data.ishide = this._ishide;

    if (overridecurrencycd) {
        data.overridecurrencycd = overridecurrencycd;
    }

    if (overrideprice) {
        data.overrideprice = overrideprice;
    }

    if (exchrate) {
        data.exchrate = exchrate;
    }

    if (grantlist) {
        data.grantlist = grantlist;
    }

    if (isoverrideprice) {
        data.isoverrideprice = isoverrideprice;
    }

    if (isoverrideexchrate) {
        data.isoverrideexchrate = isoverrideexchrate;
    }



    MyHoldings.Utilities.LoadPage(compid, MyHoldings.Resources["ProgramDetails"], data, function () {
        self.init();
        //reset values
        self._grantlist = null;
        self._programtypeid = null;
    });
}

//Add commas on numeric values
function addCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

MyHoldings.Container.HoldingProgramDetails.TableColumns = {
    "Setup": { order: "neutral" }
}

MyHoldings.Container.HoldingProgramDetails.prototype.SortMe = function (object, counter) {

    var nextorder;

    if (counter != 1) {
        var id = $(object).attr("columnid");
        var order = $(object).attr("order");
        var cssClass = $(object).attr("class").split(' ');
    }

    if (counter == 1) { //Awards column sortable icon on load
        id = 18;
        order = 0;
        nextorder = 1;

        var firstRow = $("#gvProgramDetails tbody tr:first").val();
        if (typeof firstRow === "undefined") {

        } else {
            $("#gvProgramDetails thead th:first").attr('order', '1');
            $("#gvProgramDetails thead th:first div").attr('aria-sort', 'ascending');
            $("#gvProgramDetails thead th:first div a").append('<img src="../../Images/asc_arrow.gif" style="position: absolute; top: 7px; right: 0px;" class="ascending" alt="sortable column, sorted ascending, : activate to sort column descending">');
        }
    } else {
        $(".details-table-header tr th div > a >  img").remove();
        if (id == 18) {
            if (order == 0) {
                $('.' + cssClass[0] + ' div > a > img').remove();
                $('.' + cssClass[0] + ' div > a').append('<img src="../../Images/asc_arrow.gif" style="position: absolute; top: 7px; right: 0px;" class="ascending" alt="sortable column, sorted ascending, : activate to sort column descending">');
                $('.' + cssClass[0] + ' div > a > img[class="expand"]').remove();
                $('.' + cssClass[0] + ' div > a > img').parent().parent().attr('aria-sort', 'ascending');
                $('.' + cssClass[0] + ' div > a').focus();
                nextorder = 1;
            } else {

                $('.' + cssClass[0] + ' div > a > img').remove();
                $('.' + cssClass[0] + ' div > img[class="expand"]').remove();
                $('.' + cssClass[0] + ' div > a').append('<img src="../../Images/desc_arrow.gif" style="position: absolute; top: 7px; right: 0px;" class="descending" alt="sortable column, sorted descending, : activate to sort column ascending">');

                $('.' + cssClass[0] + ' div > a > img').parent().parent().attr('aria-sort', 'descending');
                $('.' + cssClass[0] + ' div > a').focus();
                nextorder = 0;
            }
        }
        else if (order == 0) {

            $('.' + cssClass[0] + ' div > img').remove();
            $('.' + cssClass[0] + ' div > a > img[class="expand"]').remove();
            $('.' + cssClass[0] + ' div > a').append('<img src="../../Images/asc_arrow.gif" style="position: absolute; top: 7px; right: 0px;" class="ascending" alt="sortable column, sorted ascending, : activate to sort column descending">');
            $('.' + cssClass[0] + ' div > a > img').parent().parent().attr('aria-sort', 'ascending');
            nextorder = 1;
            order = 1;
            $('.' + cssClass[0] + ' div > a').focus();
        } else {

            $('.' + cssClass[0] + ' div > a > img').remove();
            $('.' + cssClass[0] + ' div > a > img[class="expand"]').remove();
            $('.' + cssClass[0] + ' div > a').append('<img src="../../Images/desc_arrow.gif" style="position: absolute; top: 7px; right: 0px;" class="descending" alt="sortable column, sorted descending, : activate to sort column ascending">');
            $('.' + cssClass[0] + ' div > a > img').parent().parent().attr('aria-sort', 'descending');
            nextorder = 0;
            order = 0;
            $('.' + cssClass[0] + ' div > a').focus();
        }
    }



    $(object).attr("order", nextorder);

    var sorting = [[id, order]];
    $("#gvProgramDetails").trigger("sorton", [sorting]);

}

MyHoldings.Container.HoldingProgramDetails.prototype.SortUpdate = function (columnid, data) {
    $("#Tablename:columnid").attr("", data["order"]);
}
//START: Key Exec Toggle
var obj = {};
obj.OrigVal = [];

//PWD
MyHoldings.Container.HoldingProgramDetails.AriaHiddenTrue = function (value) {
    if (value == "lnkSettings") {
        $(".details-grid-group").attr('aria-hidden', 'true');
        $(".details-grid-header").attr('aria-hidden', 'true');
    }
   

}

MyHoldings.Container.HoldingProgramDetails.styleDiv = function (value) {
    if (value == "program-details-contents") {
        $(".program-details-contents").css('margin-top', '30px');
    }
}
//

MyHoldings.Container.HoldingProgramDetails.prototype.KeyExec = function (xchng) {
    //[R30.0.01] ACS
    var totalUnvested = 0;
    var totalSharesReleasing = 0;
    totalAvailToSell = 0;

    var _totalAffectedUnreleased = 0;
    var otherRSUTotal = 0;
    var otherGrantsTotal = 0;
    var grantsTotal = 0;
    var RSUTotal = 0;
    var _totalAffectedAvailableToSell = 0;
    var _otherRSUAvailableToSell = 0;
    var psothergrants_availToSell = 0;
    var _otherGrantsAvailableToSell = 0;
    var availableToSellTotal = 0;
    var sharesOutstandingTotal = 0;
    //var _Settingsx = new MyHoldings.Container.Settings("settingscontainer");
    var currcode = updatedcurrencycd; //$("#hidCurrencyCd").attr("value"); //Get Currency Code  -- updated value based from 05.HoldingProgramSummary.js
    var exchangerate = updatedexchangerate; //1 //R30.0.01: Key Exec Toggle -- updated value based from 05.HoldingProgramSummary.js
    var ovrdshrprcechanged06 = ovrdshrprcechanged; // R30.0.01: Key Exec Toggle -- handler if share price is updated
    if (xchng != null) {
        exchangerate = xchng;
    }
    getOrigValue();

    function computeUnreleased(grantyear, x, pid) {
        var percentage;
        var _targetVestTotal;
        var _OIPortionTarget;
        var _TSROptionTarget;
        var _OIPortionTrend;
        var _TSROptionTrend;
        var _trendVestTotal;


        if ($("#RootContainer .keyexec-fldset-box input:checked").val() == 0) {
            $('#' + pid + ' > td:nth-child(8)').html(addCommas(obj.OrigVal[x].unreleased) + '&nbsp;&nbsp;&nbsp;&nbsp;');
            if (obj.OrigVal[x].sharesReleasing != 0) {
                $('#' + pid + ' > td:nth-child(6)').html(addCommas(obj.OrigVal[x].sharesReleasing) + '&nbsp;&nbsp;&nbsp;&nbsp;');
            }

            //UNRELEASED
            _totalAffectedUnreleased += obj.OrigVal[x].unreleased;

            //Available To Sell --------------------------------------------------------------------
            var psaffected_availToSell = $('#' + pid).children("td:nth-child(2)").text();

            if (psaffected_availToSell.indexOf('(') != -1) {
                psaffected_availToSell = psaffected_availToSell.replace('(', '').replace(')', '');
                var intpsaffected_availToSell = Number(psaffected_availToSell.replace(/[^0-9\.]+/g, ""));
                _totalAffectedAvailableToSell -= intpsaffected_availToSell;
            }
            else {
                var intpsaffected_availToSell = Number(psaffected_availToSell.replace(/[^0-9\.]+/g, ""));
                _totalAffectedAvailableToSell += intpsaffected_availToSell;
            }
            //-------------------------------------------------------------------------------------------------
        }
        else if ($("#RootContainer .keyexec-fldset-box input:checked").val() == 1) {

            //UNRELEASED
            _targetVestTotal = Math.round(obj.OrigVal[x].unreleased * (2 / 3));
            _OIPortionTarget = Math.round(Math.round(_targetVestTotal) * (3 / 4)); //KVP, [R30.0.01] ADT Defect 29249 
            _TSROptionTarget = Math.round(_targetVestTotal) - Math.round(_OIPortionTarget);
            $('#' + pid + ' > td:nth-child(8)').html(addCommas(_targetVestTotal) + '&nbsp;&nbsp;&nbsp;&nbsp;');
            _totalAffectedUnreleased = _totalAffectedUnreleased + _targetVestTotal;

            //Available To Sell --------------------------------------------------------------------
            var psaffected_availToSell = $('#' + pid).children("td:nth-child(2)").text();

            if (psaffected_availToSell.indexOf('(') != -1) {
                psaffected_availToSell = psaffected_availToSell.replace('(', '').replace(')', '');
                var intpsaffected_availToSell = Number(psaffected_availToSell.replace(/[^0-9\.]+/g, ""));
                _totalAffectedAvailableToSell -= intpsaffected_availToSell;
            }
            else {
                var intpsaffected_availToSell = Number(psaffected_availToSell.replace(/[^0-9\.]+/g, ""));
                _totalAffectedAvailableToSell += intpsaffected_availToSell;
            }
            //-------------------------------------------------------------------------------------------------

            //SHARES RELEASING
            _targetVestTotal = Math.round(obj.OrigVal[x].sharesReleasing * (2 / 3));
            _OIPortionTarget = Math.round(Math.round(_targetVestTotal) * (3 / 4)); //KVP, [R30.0.01] ADT Defect 29249 
            _TSROptionTarget = Math.round(_targetVestTotal) - Math.round(_OIPortionTarget);
            if (_targetVestTotal != 0) {
                $('#' + pid + ' > td:nth-child(6)').html(addCommas(_targetVestTotal) + '&nbsp;&nbsp;&nbsp;&nbsp;');
            }

        }
        else if ($("#RootContainer .keyexec-fldset-box input:checked").val() == 2) {
            var hasComputed = [];
            $(".computation-tbl > tbody > tr").each(function () {

                if (grantyear == $(this).find("td:nth-child(2)").text()) {
                    percentage = $(this).find("td:nth-child(3)").text();
                    percentage = percentage.split('~');

                    //UNRELEASED
                    _targetVestTotal = obj.OrigVal[x].unreleased * (2 / 3);
                    _OIPortionTarget = Math.round(Math.round(_targetVestTotal) * (3 / 4)); //KVP, [R30.0.01] ADT Defect 29249 
                    _TSROptionTarget = Math.round(_targetVestTotal) - Math.round(_OIPortionTarget);

                    _OIPortionTrend = Math.round(_OIPortionTarget * (parseFloat(percentage[0]) / 100));
                    _TSROptionTrend = Math.round(_TSROptionTarget * (parseFloat(percentage[1]) / 100));
                    _trendVestTotal = Math.round(_OIPortionTrend) + Math.round(_TSROptionTrend);

                    $('#' + pid + ' > td:nth-child(8)').html(addCommas(_trendVestTotal) + '&nbsp;&nbsp;&nbsp;&nbsp;');
                    _totalAffectedUnreleased = _totalAffectedUnreleased + _trendVestTotal

                    //Available To Sell --------------------------------------------------------------------
                    var psaffected_availToSell = $('#' + pid).children("td:nth-child(2)").text();

                    if (psaffected_availToSell.indexOf('(') != -1) {
                        psaffected_availToSell = psaffected_availToSell.replace('(', '').replace(')', '');
                        var intpsaffected_availToSell = Number(psaffected_availToSell.replace(/[^0-9\.]+/g, ""));
                        _totalAffectedAvailableToSell -= intpsaffected_availToSell;
                    }
                    else {
                        var intpsaffected_availToSell = Number(psaffected_availToSell.replace(/[^0-9\.]+/g, ""));
                        _totalAffectedAvailableToSell += intpsaffected_availToSell;
                    }
                    //-------------------------------------------------------------------------------------------------

                    //SHARES RELEASING
                    _targetVestTotal = obj.OrigVal[x].sharesReleasing * (2 / 3);
                    _OIPortionTarget = Math.round(Math.round(_targetVestTotal) * (3 / 4)); //KVP, [R30.0.01] ADT Defect 29249 
                    _TSROptionTarget = Math.round(_targetVestTotal) - Math.round(_OIPortionTarget);

                    _OIPortionTrend = Math.round(_OIPortionTarget * (parseFloat(percentage[0]) / 100));
                    _TSROptionTrend = Math.round(_TSROptionTarget * (parseFloat(percentage[1]) / 100));
                    _trendVestTotal = Math.round(_OIPortionTrend) + Math.round(_TSROptionTrend);

                    if (_trendVestTotal != 0) {
                        $('#' + pid + ' > td:nth-child(6)').html(addCommas(_trendVestTotal) + '&nbsp;&nbsp;&nbsp;&nbsp;');
                    }
                    return false;
                }
                else {
                    if ($(".computation-tbl > tbody > tr").find("td:nth-child(2):contains(" + grantyear + ")").text() == "") {

                        //UNRELEASED
                        _targetVestTotal = Math.round(obj.OrigVal[x].unreleased * (2 / 3));
                        _OIPortionTarget = Math.round(Math.round(_targetVestTotal) * (3 / 4)); //KVP, [R30.0.01] ADT Defect 29249 
                        _TSROptionTarget = Math.round(_targetVestTotal) - Math.round(_OIPortionTarget);
                        $('#' + pid + ' > td:nth-child(8)').html(addCommas(_targetVestTotal) + '&nbsp;&nbsp;&nbsp;&nbsp;');
                        _totalAffectedUnreleased = _totalAffectedUnreleased + _targetVestTotal

                        //Available To Sell --------------------------------------------------------------------
                        var psaffected_availToSell = $('#' + pid).children("td:nth-child(2)").text();

                        if (psaffected_availToSell.indexOf('(') != -1) {
                            psaffected_availToSell = psaffected_availToSell.replace('(', '').replace(')', '');
                            var intpsaffected_availToSell = Number(psaffected_availToSell.replace(/[^0-9\.]+/g, ""));
                            _totalAffectedAvailableToSell -= intpsaffected_availToSell;
                        }
                        else {
                            var intpsaffected_availToSell = Number(psaffected_availToSell.replace(/[^0-9\.]+/g, ""));
                            _totalAffectedAvailableToSell += intpsaffected_availToSell;
                        }
                        //-------------------------------------------------------------------------------------------------

                        //SHARES RELEASING
                        _targetVestTotal = Math.round(obj.OrigVal[x].sharesReleasing * (2 / 3));
                        _OIPortionTarget = Math.round(Math.round(_targetVestTotal) * (3 / 4)); //KVP, [R30.0.01] ADT Defect 29249 
                        _TSROptionTarget = Math.round(_targetVestTotal) - Math.round(_OIPortionTarget);
                        if (_targetVestTotal != 0) {
                            $('#' + pid + ' > td:nth-child(6)').html(addCommas(_targetVestTotal) + '&nbsp;&nbsp;&nbsp;&nbsp;');
                        }
                        return false;
                    }
                }
            });
        }
    }
    function getOrigValue() {
        var x = 0;
        var y = 0; //Y:indicate if Other grants have been selected.  
        $('#gvProgramDetails > tbody > tr').each(function () {
            var pid = $(this).attr('id');
            var vType = $(this).attr('id').split('-');
            var holdingsType = vType[3];
            var planNum = vType[2];
            var isAffected = $('#' + pid + ' > td.unrelease-ind > input').val()

            if (holdingsType == '2' || holdingsType == '4') {
                y = 1; //Y:indicate if Other grants have been selected.  
                if (isAffected == 1) {
                    //get year and percentage
                    var grantdate = $('#' + pid + '> td.grantdate-' + planNum + ' input').val();
                    var grantyear = new Date(grantdate).getFullYear();

                    var unreleased = $('#' + pid).children("td:nth-child(8)").text();
                    var intUnreleased = Number(unreleased.replace(/[^0-9\.]+/g, ""));

                    var sharesReleasing = $('#' + pid).children("td:nth-child(6)").text();
                    var intSharesReleasing = Number(sharesReleasing.replace(/[^0-9\.]+/g, ""));

                    obj.OrigVal.push({
                        "award": $('#' + pid + ' > td:nth-child(1)').text(),
                        "unreleased": intUnreleased,
                        "sharesReleasing": intSharesReleasing
                    });
                    computeUnreleased(grantyear, x, pid);
                    x++;

                } else {
                    //[R30.0.01] JEE
                    //Other RSU Total : Unreleased Column
                    var otherRSUTotalTemp = $('#' + pid).children('td:nth-child(8)').text();
                    var intOtherRSUTotalTemp = Number(otherRSUTotalTemp.replace(/[^0-9\.]+/g, ""));

                    otherRSUTotal += intOtherRSUTotalTemp;

                    var ps_availToSell = $('#' + pid).children('td:nth-child(2)').text();

                    if (ps_availToSell.indexOf('(') != -1) {
                        ps_availToSell = ps_availToSell.replace('(', '').replace(')', '');
                        var intps_availToSell = Number(ps_availToSell.replace(/[^0-9\.]+/g, ""));
                        _otherRSUAvailableToSell -= intps_availToSell;

                    }
                    else {
                        var intps_availToSell = Number(ps_availToSell.replace(/[^0-9\.]+/g, ""));
                        _otherRSUAvailableToSell += intps_availToSell;

                    }
                    //-------------------------------------------------------------------------------------------------
                }


            }
            else {
                //[R30.0.01] JEE
                //Other Grants
                var otherGrantsTemp = $('#' + pid).children('td:nth-child(8)').text();
                var intOtherGrantsTemp = Number(otherGrantsTemp.replace(/[^0-9\.]+/g, ""));

                otherGrantsTotal += intOtherGrantsTemp;

                //Other Grants available To Sell --------------------------------------------------------------------
                var psothergrants_availToSell = $('#' + pid).children('td:nth-child(2)').text();

                if (psothergrants_availToSell.indexOf('(') != -1) {
                    psothergrants_availToSell = psothergrants_availToSell.replace('(', '').replace(')', '');
                    var intpsOthergrants_availToSell = Number(psothergrants_availToSell.replace(/[^0-9\.]+/g, ""));
                    _otherGrantsAvailableToSell -= intpsOthergrants_availToSell;
                }
                else {
                    var intpsOthergrants_availToSell = Number(psothergrants_availToSell.replace(/[^0-9\.]+/g, ""));
                    _otherGrantsAvailableToSell += intpsOthergrants_availToSell;
                }
                //-------------------------------------------------------------------------------------------------
            }

            //[R30.0.01] ACS
            var availToSell = $('#' + pid).children('td:nth-child(2)').text();
            var sharesRelease = $('#' + pid).children('td:nth-child(6)').text();
            var intSharesRelease = Number(sharesRelease.replace(/[^0-9\.]+/g, ""));
            var unvestedUnrelease = $('#' + pid).children('td:nth-child(8)').text();
            var intUnvestedUnrelease = Number(unvestedUnrelease.replace(/[^0-9\.]+/g, ""));

            if (availToSell.indexOf('(') != -1) {
                availToSell = availToSell.replace('(', '').replace(')', '');
                var intAvailToSell = Number(availToSell.replace(/[^0-9\.]+/g, ""));
                totalAvailToSell -= intAvailToSell;
            }
            else {
                var intAvailToSell = Number(availToSell.replace(/[^0-9\.]+/g, ""));
                totalAvailToSell += intAvailToSell;
            }

            totalSharesReleasing += intSharesRelease;
            totalUnvested += intUnvestedUnrelease;
        });

        if (totalSharesReleasing != 0) {
            $('#gvProgramDetails > tfoot > tr > td:nth-child(6)').html(addCommas(totalSharesReleasing) + '&nbsp;&nbsp;&nbsp;&nbsp;');
        }
        //Unreleased/Unvested
        $('#gvProgramDetails > tfoot > tr > td:nth-child(8)').html(addCommas(totalUnvested) + '&nbsp;&nbsp;&nbsp;&nbsp;');

        //START: ACS, 3/17/2017, [R30.0.01] Key Exec Toggle
        //removed code due to GES requirement changes
        //declare variables
        //        if ($("#RootContainer .keyexec-fldset-box input:checked").val() == 1 && $('#gvProgramSummaryFooter > tbody > tr.summary-row-gridview.selected3').find('td:nth-child(1)').text() == 'All Programs') {
        //            var Unvested = totalUnvested;
        //            var EORLimit = $('span#ctl00_DefaultContent_HoldingsEORSummary_lblEorLimit').text();
        //            var intEORLimit = Number(EORLimit.replace(/[^0-9\.]+/g, ""));

        //            var totalShare = totalAvailToSell + Unvested;

        //            var EOR = totalShare - intEORLimit;

        //            //override totalshares
        //            $('span#ctl00_DefaultContent_HoldingsEORSummary_lblEorTotalShares').html(addCommas(totalShare));

        //            //if totalshares is greater than EORLimit, display result and Above EOR in green, else display result and Below EOR in red
        //            if (EOR != null || EOR != 0) {
        //                if (totalShare > intEORLimit) {
        //                    $('span#ctl00_DefaultContent_HoldingsEORSummary_lblEorValue').html(addCommas(EOR));
        //                    $('span#ctl00_DefaultContent_HoldingsEORSummary_lblEorStatus').html('Above EOR');
        //                }
        //                else {
        //                    $('span#ctl00_DefaultContent_HoldingsEORSummary_lblEorValue').html(addCommas(EOR)).addClass('red');
        //                    $('span#ctl00_DefaultContent_HoldingsEORSummary_lblEorStatus').html('Below EOR').addClass('red');
        //                }
        //            }
        //        }
        //END: ACS, 3/17/2017, [R30.0.01] Key Exec Toggle

        //[R30.0.01] JEE
        //Total of All RSU unreleased column;
        RSUTotal = _totalAffectedUnreleased + otherRSUTotal

        //RSU Share Outstanding Total column;
        RSUAvailableToSell = _totalAffectedAvailableToSell + _otherRSUAvailableToSell

        //All grants total: Unreleased
        grantsTotal = otherGrantsTotal + RSUTotal;


        //All grants total: Available To Sell
        availableToSellTotal = _otherGrantsAvailableToSell + RSUAvailableToSell

        //All grants Share Outstanding Total column;
        rsuSharesOutstanding = RSUAvailableToSell + RSUTotal //Unreleased(Column)

        sharesOutstandingTotal = availableToSellTotal + grantsTotal

        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //Program Summary:
        //[R30.0.01] JEE, 3/21/2017
        //RSU Summary Update    
        var totalSharesOutsanding = 0;
        var totalUnreleasedUnvested = 0;
        var totalValue = 0;
        var totalUnreleasedValue = 0;

        $('#ProgramSummary tbody#gvProgramSummary > tr').each(function () {
            var programName = $.trim($(this).find('td:first').text());

            if (programName == 'RSUs' && y == 1) {
                //Data Grid Content

                // - Available To Sell ----------------------------------------------------------------------------------------------------------
                $(this).find('td:nth-child(3)').html(addCommas(rsuSharesOutstanding) + '&nbsp;&nbsp;&nbsp;');

                // - Unreleased/Unvested ----------------------------------------------------------------------------------------------------------
                $(this).find('td:nth-child(9)').html(addCommas(RSUTotal) + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;');
            }

        });

        if (ovrdshrprcechanged06 == 0) {
            MyHoldings.Container.HoldingProgramSummary.prototype.RecomputeGrid(currcode, exchangerate, 0, 1);
        } else {
            MyHoldings.Container.HoldingProgramSummary.prototype.RecomputeGrid(currcode, exchangerate, 1, 0);
        }

        //  CALL FUNCTION FOR DETAILS TAB
        if ($("#tabs").length > 0) {
            if ($('[id$="hidgrantindicator"]').val() == 'Y') {
                MyHoldings.Container.RsuDetails.prototype.KeyExec();
                MyHoldings.Container.RsuDetailsVestSched.prototype.KeyExec();
                MyHoldings.Container.RsuDetailsReleaseSched.prototype.KeyExec();
            }
        }
    }

    $("#RootContainer .keyexec-fldset-box input:radio").unbind('click');
    $("#RootContainer .keyexec-fldset-box input:radio").click(function () {
        var _parent = $(this);
        var x = 0;
        var y = 0;
        _totalAffectedUnreleased = 0;
        otherRSUTotal = 0;
        otherGrantsTotal = 0;
        grantsTotal = 0;
        RSUTotal = 0;
        totalSharesReleasing = 0;
        totalUnvested = 0;
        RSUAvailableToSell = 0;
        rsuSharesOutstanding = 0;
        _otherGrantsAvailableToSell = 0
        RSUAvailableToSell = 0;
        availableToSellTotal = 0;
        _totalAffectedAvailableToSell = 0;
        _otherRSUAvailableToSell = 0;
        sharesOutstandingTotal = 0

        $('#gvProgramDetails > tbody > tr').each(function () {
            var pid = $(this).attr('id');
            var vType = $(this).attr('id').split('-');
            var holdingsType = vType[3];
            var planNum = vType[2];
            var unreleased;
            var percentage = 0;
            var comptYear;
            var isAffected = $('#' + pid + ' > td.unrelease-ind > input').val()

            if (holdingsType == '2' || holdingsType == '4') {
                y = 1; //Y:indicate if Other grants have been selected.  

                //RSU Grants
                //RSU Affected Grants
                if (isAffected == 1) {
                    //get year and percentage
                    var grantdate = $('#' + pid + '> td.grantdate-' + planNum + ' input').val();
                    var grantyear = new Date(grantdate).getFullYear();

                    //computation
                    computeUnreleased(grantyear, x, pid);
                    x++;
                }
                else {
                    //[R30.0.01] JEE
                    //Other RSU Total of Unreleased
                    holderOtherRSUTotal = $('#' + pid).children('td:nth-child(8)').text();
                    var intHolderOtherRSUTotal = Number(holderOtherRSUTotal.replace(/[^0-9\.]+/g, ""));
                    otherRSUTotal += intHolderOtherRSUTotal;

                    //Other RSU available To Sell --------------------------------------------------------------------
                    var ps_availToSell = $('#' + pid).children('td:nth-child(2)').text();

                    if (ps_availToSell.indexOf('(') != -1) {
                        ps_availToSell = ps_availToSell.replace('(', '').replace(')', '');
                        var intps_availToSell = Number(ps_availToSell.replace(/[^0-9\.]+/g, ""));
                        _otherRSUAvailableToSell -= intps_availToSell;
                    }
                    else {
                        var intps_availToSell = Number(ps_availToSell.replace(/[^0-9\.]+/g, ""));
                        _otherRSUAvailableToSell += intps_availToSell;
                    }
                    //-------------------------------------------------------------------------------------------------
                }

            }
            else {
                //[R30.0.01] JEE
                //Other Grants
                var otherGrantsTemp = $('#' + pid).children('td:nth-child(8)').text();
                var intOtherGrantsTemp = Number(otherGrantsTemp.replace(/[^0-9\.]+/g, ""));

                otherGrantsTotal += intOtherGrantsTemp;
                //Other Grants available To Sell --------------------------------------------------------------------
                var psothergrants_availToSell = $('#' + pid).children('td:nth-child(2)').text();

                if (psothergrants_availToSell.indexOf('(') != -1) {
                    psothergrants_availToSell = psothergrants_availToSell.replace('(', '').replace(')', '');
                    var intpsothergrants_availToSell = Number(psothergrants_availToSell.replace(/[^0-9\.]+/g, ""));
                    _otherGrantsAvailableToSell -= intpsothergrants_availToSell;
                }
                else {
                    var intpsothergrants_availToSell = Number(psothergrants_availToSell.replace(/[^0-9\.]+/g, ""));
                    _otherGrantsAvailableToSell += intpsothergrants_availToSell;
                }

                //-------------------------------------------------------------------------------------------------
            }

            //[R30.0.01] ACS
            var sharesRelease = $('#' + pid).children('td:nth-child(6)').text();
            var intSharesRelease = Number(sharesRelease.replace(/[^0-9\.]+/g, ""));
            var unvestedUnrelease = $('#' + pid).children('td:nth-child(8)').text();
            var intUnvestedUnrelease = Number(unvestedUnrelease.replace(/[^0-9\.]+/g, ""));

            totalSharesReleasing += intSharesRelease;
            totalUnvested += intUnvestedUnrelease;
        });
        //[R30.0.01] ACS
        if (totalSharesReleasing != 0) {
            $('#gvProgramDetails > tfoot > tr > td:nth-child(6)').html(addCommas(totalSharesReleasing) + '&nbsp;&nbsp;&nbsp;&nbsp;');
        }
        $('#gvProgramDetails > tfoot > tr > td:nth-child(8)').html(addCommas(totalUnvested) + '&nbsp;&nbsp;&nbsp;&nbsp;');
        //[R30.0.01] JEE
        //Total of All RSU unreleased column;
        RSUTotal = _totalAffectedUnreleased + otherRSUTotal;

        //RSU Share Outstanding Total column;
        RSUAvailableToSell = _totalAffectedAvailableToSell + _otherRSUAvailableToSell;

        //All grants total: Unreleased
        grantsTotal = otherGrantsTotal + RSUTotal;

        //All grants total: Available To Sell
        availableToSellTotal = _otherGrantsAvailableToSell + RSUAvailableToSell;

        //All grants Share Outstanding Total column;
        rsuSharesOutstanding = RSUAvailableToSell + RSUTotal;

        sharesOutstandingTotal = availableToSellTotal + grantsTotal;


        //---------------------------------------------------------------------------------------------------
        //Program Summary:
        //[R30.0.01] JEE, 3/21/2017
        //RSU Summary Update   
        var totalSharesOutsanding = 0;
        var totalUnreleasedUnvested = 0;
        var totalValue = 0;
        var totalUnreleasedValue = 0;

        //$('#ProgramSummary table#gvProgramSummary > tbody > tr').each(function () {
        $('#ProgramSummary tbody#gvProgramSummary > tr').each(function () {
            var programName = $.trim($(this).find('td:first').text());

            if (programName == 'RSUs' && y == 1) {
                //Data Grid Content

                // - Available To Sell ----------------------------------------------------------------------------------------------------------
                $(this).find('td:nth-child(3)').html(addCommas(rsuSharesOutstanding) + '&nbsp;&nbsp;&nbsp;');

                // - Unreleased/Unvested ----------------------------------------------------------------------------------------------------------
                $(this).find('td:nth-child(9)').html(addCommas(RSUTotal) + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;');
            }


        });

        if (ovrdshrprcechanged06 == 0) {
            MyHoldings.Container.HoldingProgramSummary.prototype.RecomputeGrid(currcode, exchangerate, 0, 1);
        } else {
            MyHoldings.Container.HoldingProgramSummary.prototype.RecomputeGrid(currcode, exchangerate, 1, 0);
        }

        //  CALL FUNCTION FOR DETAILS TAB
        if ($("#tabs").length > 0) {
            if ($('[id$="hidgrantindicator"]').val() == 'Y') {
                MyHoldings.Container.RsuDetails.prototype.KeyExec();
                MyHoldings.Container.RsuDetailsVestSched.prototype.KeyExec();
                MyHoldings.Container.RsuDetailsVestSchedInfo.prototype.KeyExec();
                MyHoldings.Container.RsuDetailsReleaseSched.prototype.KeyExec();
                MyHoldings.Container.RsuDetailsReleaseSchedInfo.prototype.KeyExec();
            }
        }
    });
    //END: Key Exec Toggle
}
MyHoldings.Container.HoldingProgramDetails.Focus = function (e) {
    if (e == "FocusOut") {
        $("#forwardback-btn").children("img").css('border','none');
    }
}

function getBrowserInfo() {
    var ua = navigator.userAgent, tem,
	M = ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || [];
    if (/trident/i.test(M[1])) {
        tem = /\brv[ :]+(\d+)/g.exec(ua) || [];
        return 'IE ' + (tem[1] || '');
    }
    if (M[1] === 'Chrome') {
        tem = ua.match(/\b(OPR|Edge)\/(\d+)/);
        if (tem != null) return tem.slice(1).join(' ').replace('OPR', 'Opera');
    }
    M = M[2] ? [M[1], M[2]] : [navigator.appName, navigator.appVersion, '-?'];
    if ((tem = ua.match(/version\/(\d+)/i)) != null)
        M.splice(1, 1, tem[1]);
    return M.join(' ');
}	
