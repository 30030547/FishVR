# Opencv is cv2, which is what we will be using for tracking the fish.
import cv2

from tracker import *
# Create tracker object
tracker = EuclideanDistTracker()

#Create a capture object to read the frames from the video.
cap = cv2.VideoCapture("Fish-Main-2.mp4")

#Object detection from a stable camera
object_detector = cv2.createBackgroundSubtractorMOG2(history=100, varThreshold=55)



#Start a loop to extract the frames
#Once we show the frame in real time we need to also make a key event if we want to close the video in this case 27.
while True:
    ret, frame = cap.read()
    height, width, _ = frame.shape
    print (height, width)

    #Extractregion of intrest.
    roi = frame[100: 700,300: 1025]
    

    # 1. Object Detection
    mask = object_detector.apply(roi)
    _, mask = cv2.threshold(mask, 254, 255, cv2.THRESH_BINARY)

    contours, _ = cv2.findContours(mask, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)
    detections = []
    for cnt in contours:
        #calculate the area and remove any small elements.
        area = cv2.contourArea(cnt)
        if area > 100:
         #cv2.drawContours(roi, [cnt], -1, (0, 255, 0), 2)
         x, y, w, h = cv2.boundingRect(cnt)
         cv2.rectangle(roi, (x, y), (x + w, y + h), (0, 255, 0), 3)

         detections.append([x, y, w, h])
    
    #2. Object Tracking
    boxes_ids = tracker.update(detections)
    for box_id in boxes_ids:
        x, y, w, h, id = box_id
        cv2.putText(roi, str(id), (x, y - 15), cv2.FONT_HERSHEY_PLAIN, 2, (255, 0, 0), 2)
        cv2.rectangle(roi, (x, y), (x + w, y + h), (0, 255, 0), 3)
    print(detections)

    cv2.imshow("roi", roi)
    cv2.imshow("Frame", frame)
    cv2.imshow("Mask", mask)

    key = cv2.waitKey(30)
    if key == 27:
        break

cap.release()
cv2.destroyAllWindows()