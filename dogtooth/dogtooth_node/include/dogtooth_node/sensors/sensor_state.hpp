#ifndef DOGTOOTH_NODE__SENSORS__SENSOR_STATE_HPP_
#define DOGTOOTH_NODE__SENSORS__SENSOR_STATE_HPP_

#include <dogtooth_msgs/msg/sensor_state.hpp>

#include <memory>
#include <string>

#include "dogtooth_node/sensors/sensors.hpp"

namespace robotis
{
  namespace dogtooth
  {
    namespace sensors
    {
      class SensorState : public Sensors
      {
        public:
          explicit SensorState(
            std::shared_ptr<rclcpp::Node> & nh,
            const std::string & topic_name = "sensor_state",
            const uint8_t & bumper_forward = 0,
            const uint8_t & bumper_backward = 0);

          void publish(const rclcpp::Time & now) override;

        private:
          rclcpp::Publisher<dogtooth_msgs::msg::SensorState>::SharedPtr pub_;

          uint8_t bumper_forward_;
          uint8_t bumper_backward_;
          uint8_t illumination_;
          uint8_t cliff_;
          uint8_t sonar_;
      };
    }  // namespace sensors
  }  // namespace dogtooth
}  // namespace robotis
#endif  // DOGTOOTH_NODE__SENSORS__SENSOR_STATE_HPP_
