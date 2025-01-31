//*****************************************************************************
//** 827. Making A Large Island                                     leetcode **
//*****************************************************************************

#define DIRS 4
int directions[DIRS][2] = {{1, 0}, {0, 1}, {-1, 0}, {0, -1}};

int isValidIdx(int r, int c, int R, int C) {
    return r >= 0 && r < R && c >= 0 && c < C;
}

int paint(int** grid, int R, int C, int r, int c, int color) {
    if (!isValidIdx(r, c, R, C) || grid[r][c] != 1) return 0;
    grid[r][c] = color;
    int area = 1;
    for (int i = 0; i < DIRS; i++) {
        area += paint(grid, R, C, r + directions[i][0], c + directions[i][1], color);
    }
    return area;
}

int paintAll(int** grid, int R, int C, int* areas) {
    int color = 2, maxArea = 0;
    for (int r = 0; r < R; r++) {
        for (int c = 0; c < C; c++) {
            if (grid[r][c] == 1) {
                int area = paint(grid, R, C, r, c, color);
                areas[color] = area;
                maxArea = area > maxArea ? area : maxArea;
                color++;
            }
        }
    }
    return maxArea;
}

int findMaxArea(int** grid, int R, int C, int* areas) {
    int maxArea = 0;
    for (int r = 0; r < R; r++) {
        for (int c = 0; c < C; c++) {
            if (grid[r][c] == 0) {
                int area = 1;
                int uniqueColors[DIRS];
                int colorCount = 0;
                for (int i = 0; i < DIRS; i++) {
                    int nr = r + directions[i][0];
                    int nc = c + directions[i][1];
                    if (isValidIdx(nr, nc, R, C) && grid[nr][nc] > 1) {
                        int color = grid[nr][nc];
                        int found = 0;
                        for (int j = 0; j < colorCount; j++) {
                            if (uniqueColors[j] == color) {
                                found = 1;
                                break;
                            }
                        }
                        if (!found) {
                            uniqueColors[colorCount++] = color;
                            area += areas[color];
                        }
                    }
                }
                if (area > maxArea) {
                    maxArea = area;
                }
            }
        }
    }
    return maxArea;
}

int largestIsland(int** grid, int gridSize, int* gridColSize) {
    int R = gridSize, C = gridColSize[0];
    int* areas = (int*)calloc(R * C + 2, sizeof(int));
    int maxPaintedArea = paintAll(grid, R, C, areas);
    if (maxPaintedArea == R * C) {
        free(areas);
        return maxPaintedArea;
    }
    int maxArea = findMaxArea(grid, R, C, areas);
    free(areas);
    return maxArea;
}
