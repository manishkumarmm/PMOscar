<%@ Page Language="C#" MasterPageFile="~/PMOMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="OptionalHolidays.aspx.cs" Inherits="PMOscar.OptionalHolidays" %>

<asp:Content ContentPlaceHolderID="cntBody" runat="server">
    <link href="Style/jquery-ui-1.8.12.custom.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.5.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.8.12.custom.min.js" type="text/javascript"></script>

    <style>
        /* This is the style for the trigger icon. The margin-bottom value causes the icon to shift down to center it. */
        .ui-datepicker-trigger {
            margin-left: 5px;
            margin-top: 8px;
            margin-bottom: -3px;
        }

        .table-heading {
            font-size: 20px;
        }

        #holidayTable {
            margin-top: 25px;
            font-size: 15px;
        }

            #holidayTable tr {
                padding-bottom: 20px;
                width: 250px;
                display: flex;
                justify-content: space-around;
            }

                #holidayTable tr td input, #holidayTable tr td select {
                    font-size: 13px;
                }

        .save-btn {
            font-size: 14px;
            padding: 8px 45px;
            background-color: #368ac8;
            color: #fff;
            border: none;
            border-radius: 4px;
        }

        #holidayTable tr td #viewOh {
            padding: 4px 15px;
        }

        .showEmployeeOhList {
            display: none;
        }

        .holidayHeading {
            text-align: left;
            width: 50%;
            font-weight: bold;
            font-size: 15px;
            border-bottom: 1px solid #ccc;
            padding: 2px 0px;
        }

        .tableContainer {
            width: 50%;
            display: flex;
        }

        .tableDiv {
            width: 60%;
        }

            .tableDiv a {
                text-decoration: underline;
            }


        .linkDiv {
            width: 40%;
            display: flex;
            flex-flow: column;
            margin-top: 60px;
        }

            .linkDiv a {
                float: left;
                margin-bottom: 20px;
                text-decoration: underline;
                font-size: 15px;
            }

        .yearDiv {
            width: 158px;
        }

            .yearDiv #year {
                width: 100%;
                height: 38px;
                border-radius: 5px;
                padding: 0px 8px;
            }

        .pad-10 {
            padding: 10px 0px;
        }

        #holidayTable .inputStyle {
            height: 36px;
            border-radius: 5px;
            border: 1px solid #a9a9a9;
            width: 140px;
            padding: 0px 8px;
        }
    </style>

    <%--Script section--%>
    <script type="text/javascript" language="javascript">

        //read from config
        var noOfOptionalHolidays = parseInt(<%=ConfigurationManager.AppSettings["noOfOptionalHolidays"] %>);
        var natDays = <%=ConfigurationManager.AppSettings["fixedHolidays"] %>;
        var minOhInMonth = <%=ConfigurationManager.AppSettings["minOhInMonth"] %>;
        var year;

        function loadEmployeeOhlist() {
            var x =<% =showEmployeeOhList %>;
            if (x) {
                $(".showEmployeeOhList").css("display", "block");
            }
        }

        function loadwarnings() {

            var input = '<tr><td colspan="2"  style="color: #611818; font-size: 13px;" >- Minimum ' + minOhInMonth[0][1] + ' Optional holiday should be availed till March. </td></tr>';
            $('#warnings').append(input);
            var input2 = '<tr><td colspan="2"  style="color: #611818; font-size: 13px;" >- Minimum ' + minOhInMonth[1][1] + ' Optional holidays should be availed till June. </td></tr>';
            $('#warnings').append(input2);
            var input3 = '<tr><td colspan="2"  style="color: #611818; font-size: 13px;" >- Minimum ' + minOhInMonth[2][1] + ' Optional holidays should be availed till September. </td></tr>';
            $('#warnings').append(input3);

        }

        function loadOhDropdown() {
            removeExistingOptions();
            for (var i = 1; i <= noOfOptionalHolidays; i++) {
                var Id = "#oh" + i;
                var input = '<tr id="ohtr' + i + '"><td class="pad-10"> OH ' + i + ' </td><td> <input type="text" class="inputStyle" id="oh' + i + '" value="Choose a Date" readonly="readonly" /> </td></tr>';
                $('#holidayTable').append(input);

                //add date picker 
                $(Id).datepicker({
                    beforeShowDay: nationalDays,
                    dateFormat: 'dd-M-yy',
                    defaultDate: new Date(year, 0, 01)
                });
            }
        }

        function removeExistingOptions() {
            for (var i = 1; i <= noOfOptionalHolidays; i++) {
                var Id = "table#holidayTable tr#ohtr" + i;
                $(Id).remove();
            }
        }
        //disable national holidays
        function nationalDays(date) {
            //avoid sat and sun
            if (date.getDay() == 0 || date.getDay() == 6) {
                return [false];
            }
            if (date.getFullYear() != year) {
                return [false];
            }

            //avoid fixed holidays
            for (i = 0; i < natDays.length; i++) {
                if (date.getMonth() == natDays[i][0] - 1
                    && date.getDate() == natDays[i][1]) {
                    return [false];
                }
            }
            return [true];
        }


        // Add items to dropdown
        $(function () {
            var currentYear = (new Date).getFullYear();
            var nextYear = (new Date).getFullYear() + 1;
            //$('#year').append('<option value=' + currentYear + '>' + currentYear + '</option>');
            $('#year').append('<option value=' + nextYear + '>' + nextYear + '</option>');
        });

        //get all selected oh 
        function getSelectedOhlist() {
            var selectedOh = [];
            for (var i = 1; i <= noOfOptionalHolidays; i++) {
                var Id = "#oh" + i;
                var value = $(Id).val();
                if (value == "Choose a Date") {
                    alert("Select " + noOfOptionalHolidays + " optional holidays.")
                    return false;
                }
                selectedOh.push(value);
            }
            return validateDates(selectedOh);
        }

        var sortedOh, sortedTotalHolidays;
        //validate dates.
        function validateDates(selectedOh) {
            sortedOh = [];
            sortedOh = selectedOh.sort(sortDates);

            var repeatedDates = isRepeatedDates(sortedOh);
            if (!repeatedDates) {
                return false;
            }

            var diffDays = consecutiveDays(sortedOh);
            if (!diffDays) {
                return false;
            }

            var monthAllowedOh = <%=ConfigurationManager.AppSettings["inMonthAllowedOh"] %>;
            var datesInThisMonth = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

            var month = 0;
            var sortedOhLen = sortedOh ? sortedOh.length : 0;
            for (var i = 0; i < sortedOhLen; i++) {
                var month = new Date(sortedOh[i]).getMonth();
                datesInThisMonth[month] = datesInThisMonth[month] + 1;
            }
            var optionalHolidaysSum = 0;

            optionalHolidaysSum = datesInThisMonth[0];
            if (optionalHolidaysSum > monthAllowedOh[0]) {
                alert("You cannot select more than " + monthAllowedOh[0] + " Optional holidays in January.");
                return false;
            }

            optionalHolidaysSum = optionalHolidaysSum + datesInThisMonth[1];
            if (optionalHolidaysSum > monthAllowedOh[1]) {
                alert("You cannot select more than " + monthAllowedOh[1] + " Optional holidays upto February.");
                return false;
            }

            optionalHolidaysSum = optionalHolidaysSum + datesInThisMonth[2];
            if (optionalHolidaysSum > monthAllowedOh[2]) {
                alert("You cannot select more than " + monthAllowedOh[2] + " Optional holidays upto March.");
                return false;
            }

            if (optionalHolidaysSum < minOhInMonth[0][1]) {
                alert("Optional holidays must be spread over the year. Minimum " + minOhInMonth[0][1] + " Optional holiday should be availed till March.");
                return false;
            }

            optionalHolidaysSum = optionalHolidaysSum + datesInThisMonth[3];
            if (optionalHolidaysSum > monthAllowedOh[3]) {
                alert("You cannot select more than " + monthAllowedOh[3] + " Optional holidays upto April.");
                return false;
            }

            optionalHolidaysSum = optionalHolidaysSum + datesInThisMonth[4];
            if (optionalHolidaysSum > monthAllowedOh[4]) {
                alert("You cannot select more than " + monthAllowedOh[4] + " Optional holidays upto May.");
                return false;
            }

            optionalHolidaysSum = optionalHolidaysSum + datesInThisMonth[5];
            if (optionalHolidaysSum > monthAllowedOh[5]) {
                alert("You cannot select more than " + monthAllowedOh[5] + " Optional holidays upto June.");
                return false;
            }

            if (optionalHolidaysSum < minOhInMonth[1][1]) {
                alert("Optional holidays must be spread over the year. Minimum " + minOhInMonth[1][1] + " Optional holidays should be availed till June.");
                return false;
            }

            optionalHolidaysSum = optionalHolidaysSum + datesInThisMonth[6];
            if (optionalHolidaysSum > monthAllowedOh[6]) {
                alert("You cannot select more than " + monthAllowedOh[6] + " Optional holidays upto July.");
                return false;
            }

            optionalHolidaysSum = optionalHolidaysSum + datesInThisMonth[7];
            if (optionalHolidaysSum > monthAllowedOh[7]) {
                alert("You cannot select more than " + monthAllowedOh[7] + " Optional holidays upto August.");
                return false;
            }

            optionalHolidaysSum = optionalHolidaysSum + datesInThisMonth[8];
            if (optionalHolidaysSum > monthAllowedOh[8]) {
                alert("You cannot select more than " + monthAllowedOh[8] + " Optional holidays upto September.");
                return false;
            }

            if (optionalHolidaysSum < minOhInMonth[2][1]) {
                alert("Optional holidays must be spread over the year. Minimum " + minOhInMonth[2][1] + " Optional holidays should be availed till September.");
                return false;
            }

            optionalHolidaysSum = optionalHolidaysSum + datesInThisMonth[9];
            if (optionalHolidaysSum > monthAllowedOh[9]) {
                alert("You can not select more than " + monthAllowedOh[9] + " Optional holidays upto October.");
                return false;
            }

            optionalHolidaysSum = optionalHolidaysSum + datesInThisMonth[10];
            if (optionalHolidaysSum > monthAllowedOh[10]) {
                alert("You cannot select more than " + monthAllowedOh[10] + " Optional holidays upto November.");
                return false;
            }

            optionalHolidaysSum = optionalHolidaysSum + datesInThisMonth[11];
            if (optionalHolidaysSum > monthAllowedOh[11]) {
                alert("You cannot select more than " + monthAllowedOh[11] + " Optional holidays upto December.");
                return false;
            }
            return true;
        }

        //sort dates
        function sortDates(a, b) {
            return new Date(a) - new Date(b);
        }

        //find is there more than 2 holidays in same week
        function consecutiveDays(sortedOh) {
            var totalHolidays = [], differnceInDates = {};
            var fixedHolidays = getFixedHolidays();
            totalHolidays = sortedOh.concat(fixedHolidays);
            sortedTotalHolidays = totalHolidays.sort(sortDates);
            var len = sortedTotalHolidays.length;
            for (var i = 0; i < len; i++) {
                var week = getWeekNumber(sortedTotalHolidays[i]);
                differnceInDates[week] = differnceInDates[week] ? differnceInDates[week] + 1 : 1;
                if (differnceInDates[week] > 2) {
                    alert("You cannot apply more than 2 holidays in same week, including fixed holidays.");
                    return false;
                }
            }
            return true;
        }

        //get week number
        function getWeekNumber(date) {
            var d = new Date(date);
            d.setUTCDate(d.getUTCDate() - d.getUTCDay());
            var yearStart = new Date(Date.UTC(year, 0, 1));
            return Math.ceil((((d - yearStart) / 86400000) + 1) / 7);
        };


        function getFixedHolidays() {
            var fixedHolidays = [];
            for (i = 0; i < natDays.length; i++) {
                var date = new Date(year, natDays[i][0] - 1, natDays[i][1]);
                if (date.getDay() != 0 && date.getDay() != 6) {
                    fixedHolidays.push((new Date(year, natDays[i][0] - 1, natDays[i][1])).toString('dd-M-yy'));
                }
            }
            return fixedHolidays;
        }

        // check repeated dates
        function isRepeatedDates(sortedOh) {
            var duplicateDates = [];
            var sortedArrayLen = sortedOh.length - 1;
            for (var i = 0; i < sortedArrayLen; i++) {
                if (sortedOh[i + 1] == sortedOh[i]) {
                    duplicateDates.push(sortedOh[i]);
                }
            }

            if (duplicateDates.length > 0) {
                alert("Duplicate dates found.")
                return false;
            }
            return true;
        }

        //on click of save
        $(document).ready(function () {
            loadwarnings();
            loadEmployeeOhlist();
            $("#year").change(function () {
                year = $(this).val();
                loadOhDropdown();
            });

            $("#save").click(function () {
                if (validateGo()) {
                    if (getSelectedOhlist()) {
                        FreezeOptionalHolidays();
                    }
                }
            });
            $("#viewOh").click(function () {
                var optionalHolidyListName = 'OptionalHolidayList/HOLIDAYLIST.pdf';
                window.open(optionalHolidyListName, '_blank');
            });

            $("#employeesOhList").click(function () {
                downloadEmployeeOhList()
            });
            $("#employeesOhDetailedList").click(function () {
                downloadEmployeeOhDetailedList();
            });
            $("#OhListForHRMSUpload").click(function () {
                downloadOhListForHRMSUpload();
            });
        });

        //save optional holidays
        function FreezeOptionalHolidays() {
            var obj = {
                oh: sortedOh,
                year: year
            }
            var data = JSON.stringify(obj)
            $.ajax({
                type: "POST",
                url: "OptionalHolidays.aspx/saveOptionalHolidays",
                data: data,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (result) {
                    if (result.d == true) {
                        $(".inputStyle").attr("disabled", true);
                        alert("Saved successfully.");

                    }
                    else {
                        alert("Already Submitted.")
                    }
                },
                error: function (result) {
                    alert("Unable to save your optional holidays.")
                }
            });
        }

        //validation for GO button (Report)
        function validateGo() {
            var value = $('#year').val();
            if (value == "None") {
                alert("Please select a year.");
                return false;
            }
            return true;
        }

        //download employee oh list
        function downloadEmployeeOhList() {
            $.ajax({
                type: "POST",
                url: "OptionalHolidays.aspx/downloadEmployeeHolidays",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (result) {
                    if (result.d) {
                        var hiddenElement = document.createElement('a');
                        hiddenElement.href = 'data:text/csv;charset=utf-8,' + encodeURI(result.d);
                        hiddenElement.target = '_blank';
                        hiddenElement.download = 'Employees Optional Holiday List.csv';
                        hiddenElement.click();

                    }
                    else {
                        alert("Unable to download.")
                    }
                },
                error: function (result) {
                    alert("Unable to download.")
                }
            });
        }
        function downloadEmployeeOhDetailedList() {
            $.ajax({
                type: "POST",
                url: "OptionalHolidays.aspx/downloadEmployeeHolidaysDetail",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (result) {
                    if (result.d) {
                        var hiddenElement = document.createElement('a');
                        hiddenElement.href = 'data:text/csv;charset=utf-8,' + encodeURI(result.d);
                        hiddenElement.target = '_blank';
                        hiddenElement.download = 'OH List For Manager Review.csv';
                        hiddenElement.click();

                    }
                    else {
                        alert("Unable to download.")
                    }
                },
                error: function (result) {
                    alert("Unable to download.")
                }
            });
        }

        function downloadOhListForHRMSUpload() {
            $.ajax({
                type: "POST",
                url: "OptionalHolidays.aspx/downloadOhListForHRMSUpload",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (result) {
                    if (result.d) {
                        var hiddenElement = document.createElement('a');
                        hiddenElement.href = 'data:text/csv;charset=utf-8,' + encodeURI(result.d);
                        hiddenElement.target = '_blank';
                        hiddenElement.download = 'OH List For HRMS Upload.csv';
                        hiddenElement.click();

                    }
                    else {
                        alert("Unable to download.")
                    }
                },
                error: function (result) {
                    alert("Unable to download.")
                }
            });
        }
    </script>
    <div>
        <form action="/">
            <div class="holidayHeading">Optional Holidays</div>
            <div class="tableContainer">
                <div class="tableDiv">
                    <table id="holidayTable">
                        <tr>
                            <td class="pad-10">Year</td>
                            <td class="yearDiv">
                                <select id="year">
                                    <option value="None" disabled selected>Select Year</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="javascript:void(0)" id="viewOh">View Optional Holiday List</a>
                            </td>
                        </tr>
                    </table>
                    <table id="warnings">
                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <th colspan="2">
                                <input type="button" class="save-btn" value="Save" id="save" />
                            </th>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="color: #611818; font-size: 13px;">Notes :</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="color: #611818; font-size: 13px;">- You cannot update optional holidays once it is submitted.
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="linkDiv">
                    <div class="showEmployeeOhList"><a href="javascript:void(0)" id="employeesOhList">Employees OH List</a></div>
                  <%--  <div class="showEmployeeOhList"><a href="javascript:void(0)" id="OhList">OH List</a></div>--%>
                    <div class="showEmployeeOhList"><a href="javascript:void(0)" id="employeesOhDetailedList">OH List For Manager Review</a></div>
                    <div class="showEmployeeOhList"><a href="javascript:void(0)" id="OhListForHRMSUpload">OH List For HRMS Upload</a></div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cntHead" runat="server">
</asp:Content>
