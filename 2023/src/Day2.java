import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.HashMap;
import java.util.Map;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Day2 {

    private static Map<String, Integer> colorToID = new HashMap<>();

    static {
        colorToID.put("green", 1);
        colorToID.put("blue", 2);
        colorToID.put("red", 3);
    }

    private static boolean checkThresholds(String line) {
        String[] parts = line.split(":");
        if (parts.length != 2) {
            return false;
        }

        String[] colorsData = parts[1].split(";");
        for (String colorData : colorsData) {
            String[] colorValues = colorData.trim().split(",");
            for (String val : colorValues) {
                val = val.trim();
                if (val.contains(" ")) {
                    String[] countColor = val.split(" ");
                    int count;
                    try {
                        count = Integer.parseInt(countColor[0].trim());
                    } catch (NumberFormatException e) {
                        continue;
                    }
                    int color = getColorID(countColor[1].trim());
                    if (!belowThreshold(color, count)) {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    private static boolean belowThreshold(int color, int count) {
        Map<Integer, Integer> thresholds = new HashMap<>();
        thresholds.put(1, 13); // Green
        thresholds.put(2, 14); // Blue
        thresholds.put(3, 12); // Red

        Integer threshold = thresholds.get(color);
        return threshold != null && count <= threshold;
    }

    private static int getColorID(String color) {
        return colorToID.getOrDefault(color, 0);
    }

    public static void main(String[] args) {
        String fileName = "input.txt";
        int gameSum = 0;

        try (BufferedReader br = new BufferedReader(new FileReader(fileName))) {
            String line;
            while ((line = br.readLine()) != null) {
                if (checkThresholds(line)) {
                    int gameID = extractGameID(line);
                    System.out.println("correct game ID: " + gameID);
                    gameSum += gameID;
                    System.out.println("new sum: " + gameSum);
                }
            }
            System.out.println("Final Sum of Game IDs: " + gameSum);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private static int extractGameID(String line) {
        Pattern pattern = Pattern.compile("Game (\\d+):");
        Matcher matcher = pattern.matcher(line);
        if (matcher.find()) {
            return Integer.parseInt(matcher.group(1));
        }
        return 0;
    }
}

