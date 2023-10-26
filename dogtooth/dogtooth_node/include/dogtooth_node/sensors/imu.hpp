#ifndef DOGTOOTH_NODE__SENSORS__IMU_HPP_
#define DOGTOOTH_NODE__SENSORS__IMU_HPP_

#include <memory>
#include <string>

#include <sensor_msgs/msg/imu.hpp>
#include <sensor_msgs/msg/magnetic_field.hpp>

#include "dogtooth_node/sensors/sensors.hpp"

namespace robotis
{
  namespace dogtooth
  {
    namespace sensors
    {
      class Imu : public Sensors
      {
        public:
          explicit Imu(
              std::shared_ptr<rclcpp::Node> & nh,
              const std::string & imu_topic_name = "imu",
              const std::string & mag_topic_name = "magnetic_field",
              const std::string & frame_id = "imu_link");

          void publish(const rclcpp::Time & now) override;

        private:
          rclcpp::Publisher<sensor_msgs::msg::Imu>::SharedPtr imu_pub_;
          rclcpp::Publisher<sensor_msgs::msg::MagneticField>::SharedPtr mag_pub_;
      };
    }  // namespace sensors
  }  // namespace dogtooth
}  // namespace robotis
#endif  // DOGTOOTH_NODE__SENSORS__IMU_HPP_
