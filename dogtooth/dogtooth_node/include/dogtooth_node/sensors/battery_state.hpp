#ifndef DOGTOOTH_NODE__SENSORS__BATTERY_STATE_HPP_
#define DOGTOOTH_NODE__SENSORS__BATTERY_STATE_HPP_

#include <sensor_msgs/msg/battery_state.hpp>
#include <memory>
#include <string>
#include "dogtooth_node/sensors/sensors.hpp"

namespace robotis
{
  namespace dogtooth
  {
    namespace sensors
    {
      class BatteryState : public Sensors
      {
        public:
          explicit BatteryState(std::shared_ptr<rclcpp::Node> & nh, const std::string & topic_name = "battery_state");
          void publish(const rclcpp::Time & now) override;

        private:
          rclcpp::Publisher<sensor_msgs::msg::BatteryState>::SharedPtr pub_;
      };
    }  // namespace sensors
  }  // namespace dogtooth
}  // namespace robotis
#endif  // DOGTOOTH_NODE__SENSORS__BATTERY_STATE_HPP_
