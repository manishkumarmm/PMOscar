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
    </style>

    <%--Script section--%>
    <script type="text/javascript" language="javascript">

        //read from config
        var noOfOptionalHolidays =  parseInt(<%=ConfigurationManager.AppSettings["noOfOptionalHolidays"] %>);
        var natDays = <%=ConfigurationManager.AppSettings["fixedHolidays"] %>;
        var year;

        function loadOhDropdown() {
            removeExistingOptions();
            for (var i = 1; i <= noOfOptionalHolidays; i++) {
                var Id = "#oh" + i;
                var input = '<tr id="ohtr' + i + '"><td> OH ' + i + ' </td><td> <input type="text" id="oh' + i + '" value="Choose a Date" readonly="readonly" /> </td></tr>';
                $('#holidayTable').append(input);

                //add date picker 
                $(Id).datepicker({
                    beforeShowDay: nationalDays,
                    dateFormat: 'dd-M-yy'
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
        var sortedOh;
        //validate dates.
        function validateDates(selectedOh) {
            sortedOh = [];
            sortedOh = selectedOh.sort(sortDates);

            var repeatedDates = isRepeatedDates(sortedOh);
            if (!repeatedDates) {
                return false;
            }

            var monthAllowedOh = [2, 3, 3, 4, 5, 6, 6, 7, 8, 8, 9, 9];
            var datesInThisMonth = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

            var month = 0;
            var sortedOhLen = sortedOh ? sortedOh.length : 0;
            for (var i = 0; i < sortedOhLen; i++) {
                var month = new Date(sortedOh[i]).getMonth();
                datesInThisMonth[month] = datesInThisMonth[month] + 1;
            }
            var optionalHolidaysSum = 0;

            optionalHolidaysSum = datesInThisMonth[0];
            if (optionalHolidaysSum > 2) {
                alert("You can not select more than 2 Optional holidays in January.");
                return false;
            }

            optionalHolidaysSum = optionalHolidaysSum + datesInThisMonth[1];
            if (optionalHolidaysSum > 3) {
                alert("You can not select more than 3 Optional holidays upto February.");
                return false;
            }

            optionalHolidaysSum = optionalHolidaysSum + datesInThisMonth[2];
            if (datesInThisMonth[2] > 3) {
                alert("You can not select more than 3 Optional holidays upto March.");
                return false;
            }

            optionalHolidaysSum = optionalHolidaysSum + datesInThisMonth[3];
            if (optionalHolidaysSum > 4) {
                alert("You can not select more than 4 Optional holidays upto April.");
                return false;
            }

            optionalHolidaysSum = optionalHolidaysSum + datesInThisMonth[4];
            if (optionalHolidaysSum > 5) {
                alert("You can not select more than 5 Optional holidays upto May.");
                return false;
            }

            optionalHolidaysSum = optionalHolidaysSum + datesInThisMonth[5];
            if (optionalHolidaysSum > 6) {
                alert("You can not select more than 6 Optional holidays upto June.");
                return false;
            }

            optionalHolidaysSum = optionalHolidaysSum + datesInThisMonth[6];
            if (optionalHolidaysSum > 6) {
                alert("You can not select more than 6 Optional holidays upto July.");
                return false;
            }

            optionalHolidaysSum = optionalHolidaysSum + datesInThisMonth[7];
            if (optionalHolidaysSum > 7) {
                alert("You can not select more than 7 Optional holidays upto August.");
                return false;
            }

            optionalHolidaysSum = optionalHolidaysSum + datesInThisMonth[8];
            if (optionalHolidaysSum > 8) {
                alert("You can not select more than 8 Optional holidays upto September.");
                return false;
            }

            optionalHolidaysSum = optionalHolidaysSum + datesInThisMonth[9];
            if (optionalHolidaysSum > 8) {
                alert("You can not select more than 8 Optional holidays upto October.");
                return false;
            }

            optionalHolidaysSum = optionalHolidaysSum + datesInThisMonth[10];
            if (optionalHolidaysSum > noOfOptionalHolidays) {
                alert("You can not select more than " + noOfOptionalHolidays + " Optional holidays upto November.");
                return false;
            }

            optionalHolidaysSum = optionalHolidaysSum + datesInThisMonth[11];
            if (optionalHolidaysSum > noOfOptionalHolidays) {
                alert("You can not select more than " + noOfOptionalHolidays + " Optional holidays upto December.");
                return false;
            }
            return true;
        }

        //sort dates
        function sortDates(a, b) {
            return new Date(b) - new Date(a);
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
                alert("Duplicate date.")
                return false;
            }
            return true;
        }

        //on click of save
        $(document).ready(function () {
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
            
        });

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
                    if (result.d == true)
                        alert("Saved successfully.");
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

        

    </script>
    <div>
        <form action="/">
            <table id="holidayTable">
                <tr>
                    <th colspan="2">Optional Holiday</th>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <input type="button" value="View Optional Holiday List" id="viewOh" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Year</td>
                    <td>
                        <select id="year">
                            <option value="None" disabled selected>-- Select Year--</option>
                        </select>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <th colspan="2">
                        <input type="button" value="Save" id="save" />
                    </th>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="2" style="color:red">
                        Note : You can not update optional holidays once it is submitted.
                    </td>
                </tr>
            </table>
        </form>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cntHead" runat="server">
</asp:Content>
