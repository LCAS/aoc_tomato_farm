#ifndef DOGTOOTH_NODE__SENSORS__JOINT_STATE_HPP_
#define DOGTOOTH_NODE__SENSORS__JOINT_STATE_HPP_

#include <sensor_msgs/msg/joint_state.hpp>

#include <memory>
#include <string>

#include "dogtooth_node/sensors/sensors.hpp"

namespace robotis
{
  namespace dogtooth
  {
    namespace sensors
    {
      
    constexpr uint8_t JOINT_NUM = 2;

    // ref) http://emanual.robotis.com/docs/en/dxl/x/xl430-w250/#goal-velocity104
    constexpr double RPM_TO_MS = 0.229 * 0.0034557519189487725;

    // 0.087890625[deg] * 3.14159265359 / 180 = 0.001533981f
    constexpr double TICK_TO_RAD = 0.001533981;

      class JointState : public Sensors
      {
        public:
          explicit JointState(
            std::shared_ptr<rclcpp::Node> & nh,
            const std::string & topic_name = "joint_states",
            const std::string & frame_id = "base_link");

          void publish(
            const rclcpp::Time & now) override;

        private:
          rclcpp::Publisher<sensor_msgs::msg::JointState>::SharedPtr pub_;
      };
    }  // namespace sensors
  }  // namespace dogtooth
}  // namespace robotis
#endif  // DOGTOOTH_NODE__SENSORS__JOINT_STATE_HPP_
