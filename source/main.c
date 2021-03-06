#include "graphics.h"
#include <math.h>
#include <time.h>

int isRunning = 1;

int main(int argc, char* argv[])
{
    newLine();
    newLine();
    
    ContextData contextData;
    contextData.minimalGLXVersionMajor = 1;
    contextData.minimalGLXVersionMinor = 3;
    contextData.minimalGLVersionMajor = 3;
    contextData.minimalGLVersionMinor = 3;
    contextData.windowWidth = 1920;
    contextData.windowHeight = 1080;
    contextData.name = "Faith";

    UserVSyncData userVSyncData;

    configureOpenGL(&contextData, &userVSyncData);
    loadFunctionPointers();

    
#if USE_EBO_TO_DRAW_QUAD == 1
    ScreenQuadWithEBO squad;
    configureScreenQuadWithEBO(&squad);
#else
    ScreenQuad squad;
    configureScreenQuad(&squad);
#endif

    unsigned basic = createShaderProgram("shaders/basic.vs", "shaders/basic.fs");
    glUseProgram_FA(basic);    


    glDisable(GL_DEPTH_TEST);

    disableVSyncIfPossible(&contextData, &userVSyncData);


    float iter = 1.0f;

    clock_t prevTime = clock();
    double dt = 0.0f;
    
    while(1)
    {        
        XEvent event;

        while (XPending(contextData.display))
        {
            XNextEvent(contextData.display, &event);
            switch (event.type)
            {
                case ClientMessage:
                    if (event.xclient.data.l[0] == contextData.deleteMessage)
                        isRunning = 0;
                    break;
            }
        }

        if (isRunning == 0)
        {
            break;
        }

        glClearColor(0, 0.5, 1, 1);
        glClear(GL_COLOR_BUFFER_BIT);

        int zoomLoc = glGetUniformLocation_FA(basic, "zoom");
        glUniform1f_FA(zoomLoc, 10000);

        iter += 1.0f;
        
#if USE_EBO_TO_DRAW_QUAD == 1
        glDrawElements(GL_TRIANGLE_STRIP, 6, GL_UNSIGNED_INT, 0);
#else
        glDrawArrays(GL_TRIANGLES, 0, 6);
#endif
        
        glXSwapBuffers(contextData.display, contextData.window);

        clock_t nowTime = clock();
        dt = (nowTime - prevTime) *100000.0f / CLOCKS_PER_SEC;
        prevTime = nowTime;
        //sleep(1);
    }

    freeContextData(&contextData);

#if USE_EBO_TO_DRAW_QUAD == 1
    freeScreenQuadWithEBO(&squad);
#else
    freeScreenQuad(&squad);
#endif

    glDeleteProgram_FA(basic);
    
    return 0;
}
