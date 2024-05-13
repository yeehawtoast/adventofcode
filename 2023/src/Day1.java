import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.HashMap;
import java.util.Map;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Day1 {

        private static final int MAX_INT = Integer.MAX_VALUE;

        private static Map<String, String> wordToDigit = new HashMap<>();
        private static Map<String, String> tigidDinfMap = new HashMap<>();

        static {
            wordToDigit.put("one", "1");
            wordToDigit.put("two", "2");
            wordToDigit.put("three", "3");
            wordToDigit.put("four", "4");
            wordToDigit.put("five", "5");
            wordToDigit.put("six", "6");
            wordToDigit.put("seven", "7");
            wordToDigit.put("eight", "8");
            wordToDigit.put("nine", "9");

            tigidDinfMap.put("eno", "1");
            tigidDinfMap.put("owt", "2");
            tigidDinfMap.put("eerht", "3");
            tigidDinfMap.put("ruof", "4");
            tigidDinfMap.put("evif", "5");
            tigidDinfMap.put("xis", "6");
            tigidDinfMap.put("neves", "7");
            tigidDinfMap.put("thgie", "8");
            tigidDinfMap.put("enin", "9");
        }

        private static String findDigit(String line) {
            Pattern pattern = Pattern.compile("one|two|three|four|five|six|seven|eight|nine");
            Matcher matcher = pattern.matcher(line);
            if (matcher.find()) {
                return matcher.group();
            }
            return "";
        }

        private static String tigidDinf(String line) {
            Pattern pattern = Pattern.compile("eno|owt|eerht|ruof|evif|xis|neves|thgie|enin");
            Matcher matcher = pattern.matcher(line);
            if (matcher.find()) {
                return matcher.group();
            }
            return "";
        }

        private static String reverseString(String s) {
            return new StringBuilder(s).reverse().toString();
        }

        private static String getFirstDigit(Map<Integer, String> calibrationMap) {
            String c = "";
            int i = MAX_INT;
            for (Map.Entry<Integer, String> entry : calibrationMap.entrySet()) {
                int k = entry.getKey();
                if (k < i) {
                    c = entry.getValue();
                    i = k;
                }
            }
            return c;
        }

        private static String getLastDigit(Map<Integer, String> calibrationMap) {
            String c = "";
            int i = -1;
            for (Map.Entry<Integer, String> entry : calibrationMap.entrySet()) {
                int k = entry.getKey();
                if (k > i) {
                    c = entry.getValue();
                    i = k;
                }
            }
            return c;
        }

        public static void main(String[] args) {
            String fileName = "input.txt";
            int calibrationValue = 0;

            try (BufferedReader br = new BufferedReader(new FileReader(fileName))) {
                String line;
                while ((line = br.readLine()) != null) {
                    Map<Integer, String> calibrationMap = new HashMap<>();
                    int length = line.length();

                    for (int i = 0; i < length; i++) {
                        char c = line.charAt(i);
                        if (Character.isDigit(c)) {
                            calibrationMap.put(i, String.valueOf(c));
                            break;
                        }
                    }

                    String digit = findDigit(line);
                    if (!digit.isEmpty()) {
                        calibrationMap.put(line.indexOf(digit), wordToDigit.get(digit));
                    }

                    line = reverseString(line);

                    for (int i = 0; i < length; i++) {
                        char c = line.charAt(i);
                        if (Character.isDigit(c)) {
                            calibrationMap.put(length - i - 1, String.valueOf(c));
                            break;
                        }
                    }

                    digit = tigidDinf(line);
                    if (!digit.isEmpty()) {
                        calibrationMap.put(length - line.indexOf(digit) - digit.length(), tigidDinfMap.get(digit));
                    }

                    String firstDigit = getFirstDigit(calibrationMap);
                    String lastDigit = getLastDigit(calibrationMap);
                    if (firstDigit.length() > 0 && lastDigit.length() > 0) {
                        calibrationValue += Integer.parseInt(firstDigit + lastDigit);
                    }
                }
                System.out.println("Calibration value: " + calibrationValue);
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }
