package calendar;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.LinkedList;
import java.util.Queue;
import java.util.Scanner;

public class Main {
    //Creates a String for whitespaces
    static String spaces(int number) {
        return String.format("%" + number + "s", "");
    }

    static ArrayList<LocalDateTime> createListWithMonths(int year) {
        ArrayList<LocalDateTime> months = new ArrayList<>();
        for (int i = 1; i <= 12; i++) {
            months.add(LocalDateTime.of(year, i, 1, 0, 0));
        }
        return months;
    }

    /*
     Решението с опашнка е много по тежко от гледна точка на памет, защото самата опашка не се използва
     за нищо друго освен вкарване и изкарване на елементи, което може да се постигне и с просто принтиране,
     но ми беше много полезна за визуализиране на идеята
     */


    //===============================Решение с опашка=======================================================

    //Adds days in the beginning of the month as well as all weeks except the last 2
    static void addDaysToQueue(Queue<Integer> days, ArrayList<LocalDateTime> months, int month, int endCondition) {
        for (int day = 1; day <= endCondition; day++) {
            days.add(months.get(month).getDayOfMonth());
            months.set(month, months.get(month).plusDays(1));
        }
    }

    //Adds empty spaces in the beginning or the end of a month in the form of 0s
    static void addIndentToQueue(Queue<Integer> days, int endCondition) {
        for (int indent = 0; indent < endCondition; indent++) {
            days.add(0);
        }
    }

    //Adds days in the 5th week of the month (or the 6th if there is one)
    static void addLastDaysToQueue(Queue<Integer> days, ArrayList<LocalDateTime> months, int month) {
        int counter = 0;// makes sure that if there is a 6th week you don't add it on the wrong row
        while (months.get(month).plusDays(1).getMonth().getValue() == month + 1 && counter < 6) {
            days.add(months.get(month).getDayOfMonth());
            months.set(month, months.get(month).plusDays(1));
            counter++;
        }
        days.add(months.get(month).getDayOfMonth());//Adds the last day of each month without going in the next one
    }

    // Main function for the homework: Fills the queue with days, separating the calendar into 3 main parts, each consisting of 4 months, and each month of 5 weeks(potentially 6)
    static Queue<Integer> fillQueueWithDays(ArrayList<LocalDateTime> months, int year) {
        Queue<Integer> days = new LinkedList<>();
        for (int fourMonths = 0;
             fourMonths < 3; fourMonths++) { // used to separate the calendar into 3 parts with four months
            for (int weeks = 0; weeks < 6; weeks++) { // used to separate each month in 6 weeks
                for (int month = 4 * fourMonths; month < 4 * fourMonths + 4; month++) { //used to fill the days of month
                    int dayOfTheWeek = months.get(month).getDayOfWeek()
                        .getValue(); //used in the first and last weeks of each month to see how many zero's you need

                    if (weeks == 0) {
                        if (dayOfTheWeek == 7) {
                            addDaysToQueue(days, months, month, 7);
                        } else {
                            addIndentToQueue(days, dayOfTheWeek);
                            addDaysToQueue(days, months, month, 7 - dayOfTheWeek);
                        }
                    } else if (weeks == 4) {
                        addLastDaysToQueue(days, months, month);
                        dayOfTheWeek = months.get(month).getDayOfWeek()
                            .getValue(); // updates the dayOfTheWeek in order to see how many zero's are need at the end
                        if (dayOfTheWeek == 7) {
                            addIndentToQueue(days, 6);
                        } else {
                            addIndentToQueue(days, 6 - dayOfTheWeek);
                        }
                    } else if (weeks == 5) {
                        if (months.get(month).plusDays(1).getMonth().getValue()
                            != month + 1) { // if the month is over adds 7 zero's
                            addIndentToQueue(days, 7);

                        } else {
                            months.set(month, months.get(month).plusDays(1));
                            addLastDaysToQueue(days, months, month);
                            dayOfTheWeek = months.get(month).getDayOfWeek().getValue();
                            if (dayOfTheWeek == 7) {
                                addIndentToQueue(days, 6);
                            } else {
                                addIndentToQueue(days, 6 - dayOfTheWeek);
                            }
                        }
                    } else {
                        addDaysToQueue(days, months, month, 7);

                    }
                }
            }
        }
        return days;
    }

    // Solution with queue
    /* public static void main(String[] args) {

        int year = 0;
        int betweenMonthsCounter = 0;
        Scanner in = new Scanner(System.in);
        do {
           year = in.nextInt();
        } while(year < 0);
        String daysOfWeek = "Sun Mon Tue Wed Thu Fri Sat";
        String[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September",
            "October", "November", "December"
        };
        Queue<Integer> days = fillQueueWithDays(createListWithMonths(year), year);

        System.out.println(spaces(80) + year);

        for (int fourMonths = 0; fourMonths < 3; fourMonths++) {
            for (int month = 4 * fourMonths; month < 4 * fourMonths + 4; month++) {
                System.out.print(spaces(15) + months[month] + spaces(20));
            }
            System.out.println();
            for (int i = 0; i < 4; i++) {
                System.out.print(daysOfWeek + spaces(20));
            }
            System.out.println();

            for (int weeks = 0; weeks < 6; weeks++) {
                for (int day = 0; day < 28; day++) {
                    int currentDay = days.remove();
                    betweenMonthsCounter++;
                    if (currentDay == 0) {
                        System.out.print(spaces(4));
                    } else {
                        if (currentDay >= 10) {
                            System.out.print(currentDay + spaces(2));
                        } else {
                            System.out.print(currentDay + spaces(3));
                        }
                    }
                    if (betweenMonthsCounter == 7) {
                        System.out.print(spaces(20));
                        betweenMonthsCounter = 0;
                    }
                }
                System.out.println();

            }

            System.out.println();
        }
    }*/

    //==============================Решение с просто принтиране===========================================================

    //Prints days in the beginning of the month as well as all weeks except the last 2
    static void printDays(ArrayList<LocalDateTime> months, int month, int endCondition) {
        for (int day = 1; day <= endCondition; day++) {
            int currentDay = months.get(month).getDayOfMonth();
            if (currentDay >= 10) {
                System.out.print(currentDay + spaces(2));
            } else {
                System.out.print(currentDay + spaces(3));
            }
            months.set(month, months.get(month).plusDays(1));
        }
    }

    //Prints empty spaces in the beginning or the end of a month
    static void printWhiteSpaces(int endCondition) {
        for (int indent = 0; indent < endCondition; indent++) {
            System.out.print(spaces(4));
        }
    }

    //Prints days in the 5th week of the month (or the 6th if there is one)
    static void printLastDays(ArrayList<LocalDateTime> months, int month) {
        int counter = 0;// makes sure that if there is a 6th week you don't add it on the wrong row
        while (months.get(month).plusDays(1).getMonth().getValue() == month + 1 && counter < 6) {
            System.out.print(months.get(month).getDayOfMonth() + spaces(2));
            months.set(month, months.get(month).plusDays(1));
            counter++;
        }
        System.out.print(months.get(month).getDayOfMonth() + spaces(2));
    }


    public static void main(String[] args) {

        int year = 0;
        Scanner in = new Scanner(System.in);
        do {
            year = in.nextInt();
        } while (year < 0);
        String daysOfWeek = "Sun Mon Tue Wed Thu Fri Sat";
        String[] monthNames = { "January", "February", "March", "April", "May", "June", "July", "August", "September",
            "October", "November", "December"
        };
        //Queue<Integer> days = fillQueueWithDays(createListWithMonths(year), year);
        ArrayList<LocalDateTime> months = createListWithMonths(year);
        System.out.println(spaces(80) + year);

        for (int fourMonths = 0; fourMonths < 3; fourMonths++) {
            for (int month = 4 * fourMonths; month < 4 * fourMonths + 4; month++) {
                System.out.print(spaces(10) + monthNames[month] + spaces(30));
            }
            System.out.println();
            for (int i = 0; i < 4; i++) {
                System.out.print(daysOfWeek + spaces(20));
            }
            System.out.println();

            for (int weeks = 0; weeks < 6; weeks++) {

                for (int month = 4 * fourMonths; month < 4 * fourMonths + 4; month++) { //used to fill the days of month
                    int dayOfTheWeek = months.get(month).getDayOfWeek()
                        .getValue(); //used in the first and last weeks of each month to see how many zero's you need

                    if (weeks == 0) {
                        if (dayOfTheWeek == 7) {
                            printDays(months, month, 7);
                        } else {
                            printWhiteSpaces(dayOfTheWeek);
                            printDays(months, month, 7 - dayOfTheWeek);
                        }
                    } else if (weeks == 4) {
                        printLastDays(months, month);
                        dayOfTheWeek = months.get(month).getDayOfWeek()
                            .getValue(); // updates the dayOfTheWeek in order to see how many zero's are need at the end
                        if (dayOfTheWeek == 7) {
                            printWhiteSpaces(6);
                        } else {
                            printWhiteSpaces(6 - dayOfTheWeek);
                        }
                    } else if (weeks == 5) {
                        if (months.get(month).plusDays(1).getMonth().getValue()
                            != month + 1) { // if the month is over adds 7 zero's
                            printWhiteSpaces(7);

                        } else {
                            months.set(month, months.get(month).plusDays(1));
                            printLastDays(months, month);
                            dayOfTheWeek = months.get(month).getDayOfWeek().getValue();
                            if (dayOfTheWeek == 7) {
                                printWhiteSpaces(6);
                            } else {
                                printWhiteSpaces(6 - dayOfTheWeek);
                            }
                        }
                    } else {
                        printDays(months, month, 7);

                    }
                    System.out.print(spaces(20));

                }
                System.out.println();

            }

            System.out.println();
        }
    }


}
